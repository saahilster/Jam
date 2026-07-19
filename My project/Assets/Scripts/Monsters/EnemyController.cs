using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public enum State {Patrol, Chase, Search, Flee}
public enum DetectionType {Sight, Audio}
public class EnemyController : MonoBehaviour
{
    [SerializeField] private EnemyData enemyData;
    [SerializeField] private Transform player;
    [SerializeField] private Transform patrolPointsParent;
    [SerializeField] private Transform teleportPointsParent;
    [SerializeField] private LayerMask floorMask;
    [SerializeField] private EnemySpriteAnimator spriteAnimator;
    [SerializeField] private EnemyAudioManag audioManag;
    [SerializeField] private GameObject jumpscare;
    private PlayJumpscare playJumpscare;
    private Transform[] patrolPoints;
    private Transform[] teleportPoints;

    private NavMeshAgent agent;
    private MovementBehavior currentMovement;
    private float searchTimer;
    private Vector3 lastKnownPosition;
    private float lastDetectionTime;
    private State currentState;
    private float growlTimer;
    private float roarTimer;

    private DetectionBehavior detectionBehavior;
    private DetectionBehavior chaseTracking;
    private PatrolMovement patrolMovement;

    public MicListener micListener;
    public Transform Player => player;
    public EnemyData Data => enemyData;
    public MicListener Mic => micListener;
    

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        //patrol
        patrolPoints = new Transform[patrolPointsParent.childCount];
        for (int i = 0; i < patrolPointsParent.childCount; i++)
        {
            patrolPoints[i] = patrolPointsParent.GetChild(i);
        }
        teleportPoints = new Transform[teleportPointsParent.childCount];
        for (int i = 0; i < teleportPointsParent.childCount; i++)
        {
            teleportPoints[i] = teleportPointsParent.GetChild(i);
        }
        patrolMovement = new PatrolMovement(patrolPoints);
        currentMovement = patrolMovement;
        detectionBehavior = GetDetectionBehavior(enemyData.detectionType);
        chaseTracking = new SightDetection();
        playJumpscare = GetComponent<PlayJumpscare>();
        //agent.updateRotation = false;


    }
    void Start()
    {
        if (player == null)
        {
            player = GameObject.FindWithTag("Player").transform;
        }
        Debug.Log(detectionBehavior);
        Debug.Log(currentState);
        spriteAnimator.Initialize(enemyData);

    }
    void Update()
    {
        switch (currentState)
        {
            case State.Patrol:
                spriteAnimator.SetState(currentState);
                if (detectionBehavior.CanDetectPlayer(this))
                {
                    Debug.Log("Detected Player");
                    lastKnownPosition = player.position;
                    currentState = detectionBehavior.GetReactionState();
                    currentMovement = GetMovementForState(currentState);
                    searchTimer = 0f;
                    lastDetectionTime = Time.time;
                    audioManag.StopSound(audioManag.source);
                }
                else if (IsInSameRoomAsPlayer())
                {
                    Debug.Log("In Room With Player");
                    lastKnownPosition = player.position;
                    currentState = State.Search;
                    currentMovement = GetMovementForState(currentState);
                    searchTimer = 0f;
                }
                //sound
                if (agent.velocity.magnitude != 0)
                {
                    audioManag.PlaySound(EnemySoundClip.Walking, audioManag.src2, 0.6f);
                }
                else
                {
                    audioManag.StopSound(audioManag.src2);
                }
                if (growlTimer >= 6f)
                {
                    audioManag.PlaySound(EnemySoundClip.Growl, audioManag.source, 0.2f);
                    growlTimer = Time.deltaTime;
                }
                else
                {
                    growlTimer += Time.deltaTime;
                }
                break;
            case State.Chase:
                spriteAnimator.SetState(currentState);
                
                if (chaseTracking.CanDetectPlayer(this))
                {
                    Debug.Log("Chasing Player");
                    lastKnownPosition = player.position;
                    lastDetectionTime = Time.time;
                }
                else if (Time.time - lastDetectionTime > enemyData.chaseLoseDuration)
                {
                    Debug.Log("Lost Player");
                    currentState = State.Patrol;
                    currentMovement = GetMovementForState(currentState);
                    searchTimer = 0f;
                }
                if (roarTimer >= 4.5f)
                {
                    audioManag.PlaySound(EnemySoundClip.Roar, audioManag.source, 0.8f);
                    roarTimer = 0f;
                }
                else
                {
                    roarTimer += Time.time;
                }
                audioManag.PlaySound(EnemySoundClip.Chase, audioManag.src2, 0.8f);
                break;
            case State.Search:
                searchTimer += Time.deltaTime;
                spriteAnimator.SetState(currentState);
                if (detectionBehavior.CanDetectPlayer(this))
                {
                    Debug.Log("Searching for Player");
                    lastKnownPosition = player.position;
                    currentState = State.Chase;
                    currentMovement = GetMovementForState(currentState);
                    audioManag.PlaySound(EnemySoundClip.Roar, audioManag.source, 0.6f);
                }
                else if (searchTimer >= enemyData.searchDuration)
                {
                    Debug.Log("Lost Player");
                    currentState = State.Patrol;
                    currentMovement = GetMovementForState(currentState);
                    searchTimer = 0f;
                }
                //sound
                if (agent.velocity.magnitude != 0)
                {
                    audioManag.PlaySound(EnemySoundClip.Walking, audioManag.src2, 0.6f);
                }
                else
                {
                    audioManag.StopSound(audioManag.src2);
                }
                break;
        }

        currentMovement.Move(agent, transform, player, enemyData);
        Debug.Log(currentState);
    }
    private void SetMovement(MovementBehavior newMovement)
    {
        currentMovement = newMovement;
    }
    private bool IsInSameRoomAsPlayer()
    {
        Collider enemyRoom = GetRoomFloor(transform.position, floorMask);
        Collider playerRoom = GetRoomFloor(player.position, floorMask);
            Debug.Log($"Enemy room: {(enemyRoom != null ? enemyRoom.name : "null")}, Player room: {(playerRoom != null ? playerRoom.name : "null")}");
        return enemyRoom != null && enemyRoom == playerRoom;
    }
    
    private DetectionBehavior GetDetectionBehavior(DetectionType type)
    {
        switch (type)
        {
            case DetectionType.Sight: return new SightDetection();
            case DetectionType.Audio: return new AudioDetection();
            default: return new SightDetection();
        }
    }
    private MovementBehavior GetMovementForState(State state)
    {
        switch (state)
        {
            case State.Chase: return new ChaseMovement();
            case State.Search: return new SearchMovement(GetRoomBounds(lastKnownPosition));
            case State.Patrol: return patrolMovement;
            default: return new PatrolMovement(patrolPoints);
        }
    }

    private Collider GetRoomFloor(Vector3 position, LayerMask floorMask)
    {
        Vector3 origin = position + Vector3.up * 3f;
        if (Physics.Raycast(origin, Vector3.down, out RaycastHit hit, 6f, floorMask))
        {
            if (hit.collider.CompareTag("Room"))
            {
                Debug.Log("Hit Room");
                return hit.collider;
            }
            else
            {
                Debug.Log("Hit something but tag wasn't 'Room'");
            }
        }
        else
        {
            Debug.Log($"hit nothing");

        }
        return null;
    }
    private Bounds GetRoomBounds(Vector3 position)
    {
        Collider room = GetRoomFloor(position, floorMask);
        if (room != null) return room.bounds;
        return new Bounds(position, Vector3.one * 5f);
    }
    public void TeleportRandom()
    {
        int index = Random.Range(0, teleportPoints.Length);
        agent.Warp(patrolPoints[index].position);
        currentState = State.Patrol;
        currentMovement = GetMovementForState(currentState);
    }
    public void JumpscareWoman()
    {
        audioManag.StopSound(audioManag.source);
        playJumpscare.TriggerJumpscare(jumpscare, audioManag , 0.6f, enemyData.jumpscareDuration);
    }
}
