using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private EnemyData enemyData;
    [SerializeField] private Transform player;
    [SerializeField] private Transform[] patrolPoints;

    private NavMeshAgent agent;
    private MovementBehavior currentMovement;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        currentMovement = new PatrolMovement(patrolPoints);
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
        currentMovement.Move(agent, transform, player, enemyData);
    }
    private void SetMovement(MovementBehavior newMovement)
    {
        currentMovement = newMovement;
    }
}
