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
    private Transform[] patrolPoints;

    private NavMeshAgent agent;
    private MovementBehavior currentMovement;
    private float searchTimer;
    private Vector3 lastKnownPosition;
    private float lastDetectionTime;
    private State currentState;

    private DetectionBehavior detectionBehavior;
    public DetectionType detectionType;
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
        currentMovement = new PatrolMovement(patrolPoints);
        detectionBehavior = GetDetectionBehavior(enemyData.detectionType);
        //agent.updateRotation = false;


    }
    void Start()
    {
        if (player == null)
        {
            player = GameObject.FindWithTag("Player").transform;
        }
    }
    void Update()
    {
        switch (currentState)
        {
            case State.Patrol:
                if (detectionBehavior.CanDetectPlayer(this))
                {
                    currentState = detectionBehavior.GetReactionState();
                    currentMovement = GetMovementForState(currentState);
                }
                break;
            case State.Chase:
                if (detectionBehavior.CanDetectPlayer(this))
                {
                    lastKnownPosition = player.position;
                    lastDetectionTime = Time.time;
                }
                else if (Time.time - lastDetectionTime > enemyData.chaseLoseDuration)
                {
                    currentState = State.Patrol;
                    currentMovement = GetMovementForState(currentState);
                    searchTimer = 0f;
                }
                break;
            case State.Search:
                searchTimer += Time.deltaTime;
                if (detectionBehavior.CanDetectPlayer(this))
                {
                    currentState = detectionBehavior.GetReactionState();
                    currentMovement = GetMovementForState(currentState);
                }
                else if (searchTimer >= enemyData.lostInterestTime)
                {
                    currentState = State.Patrol;
                    currentMovement = GetMovementForState(currentState);
                }
                break;
        }
        currentMovement.Move(agent, transform, player, enemyData);
    }
    private void SetMovement(MovementBehavior newMovement)
    {
        currentMovement = newMovement;
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
            //case State.Search: return new SearchMovement(lastKnownPosition);
            case State.Patrol: return new PatrolMovement(patrolPoints);
            default: return new PatrolMovement(patrolPoints);
        }
    }
}
