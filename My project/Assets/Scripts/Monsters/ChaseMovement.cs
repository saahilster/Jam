using UnityEngine;
using UnityEngine.AI;

public class ChaseMovement : MovementBehavior
{
    private Vector3 lastTargetPosition;
    public void Move(NavMeshAgent agent, Transform enemyTransform, Transform player, EnemyData data)
    {
        agent.speed = data.chaseSpeed;
        if (Vector3.Distance(lastTargetPosition, player.position) > 0.5f)
        {
            agent.SetDestination(player.position);
            lastTargetPosition = player.position;
        }
    }
}
