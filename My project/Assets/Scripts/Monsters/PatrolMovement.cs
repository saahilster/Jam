using UnityEngine;
using UnityEngine.AI;

public class PatrolMovement : MovementBehavior
{
    private Transform[] waypoints;
    private int currentIndex;
    public PatrolMovement(Transform[] waypoints)
    {
        this.waypoints = waypoints;
        currentIndex = 0;
    }
    public void Move(NavMeshAgent agent, Transform enemyTransform, Transform player, EnemyData data)
    {
        agent.speed = data.patrolSpeed;
        agent.SetDestination(waypoints[currentIndex].position);
        if (agent.remainingDistance <= agent.stoppingDistance && !agent.pathPending)
        {
            currentIndex = (currentIndex + 1) % waypoints.Length;
        }
    }
}
