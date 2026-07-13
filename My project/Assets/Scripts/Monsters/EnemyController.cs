using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private EnemyData enemyData;
    [SerializeField] private Transform player;
    [SerializeField] private Transform[] patrolPoints;
    //[SerializeField] private PlayerNoise playerNoise;
    //[SerializeField] private MicListener micListner;

    private NavMeshAgent agent;
    private MovementBehavior currentMovement;
    private float searchTimer;
    private Vector3 lastKnownPosition;
    private enum State {Patrol, Chase, Search, Flee}
    private State currentState;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        currentMovement = new PatrolMovement(patrolPoints);
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
                if (CanSeePlayer())
                {
                    currentState = State.Chase;
                    currentMovement = new ChaseMovement();
                }
                break;
            case State.Chase:
                if (CanSeePlayer())
                {
                    lastKnownPosition = player.position;
                }
                else
                {
                    currentState = State.Search;
                    //currentMovement = new SearchMovement(lastKnownPosition);
                    searchTimer = 0f;
                }
                break;
            case State.Search:
                searchTimer += Time.deltaTime;
                if (CanSeePlayer())
                {
                    currentState = State.Chase;
                    currentMovement = new ChaseMovement();
                }
                else if (searchTimer >= enemyData.lostInterestTime)
                {
                    currentState = State.Patrol;
                    currentMovement = new PatrolMovement(patrolPoints);
                }
                break;
        }
        currentMovement.Move(agent, transform, player, enemyData);
    }
    private void SetMovement(MovementBehavior newMovement)
    {
        currentMovement = newMovement;
    }
    private bool CanSeePlayer()
    {
        float distance = Vector3.Distance(transform.position, player.position);
        if (distance > enemyData.detectionRadius)
            return false;

        Vector3 directionToPlayer = (player.position - transform.position).normalized;
        float angle = Vector3.Angle(transform.forward, directionToPlayer);
        if (angle > enemyData.fieldOfViewAngle * 0.5f)
            return false;

        if (Physics.Raycast(transform.position, directionToPlayer, out RaycastHit hit, distance, enemyData.obstacleLayerMask))
        {
            return false;
        }
        return true;
    }
}
