using UnityEngine;
using UnityEngine.AI;

public class SearchMovement : MovementBehavior
{

    private float pointTimer;
    private bool waiting;
    private float waitDuration = 2f;
    private float waitTimer;
    private Bounds roomBounds;


    //Make sure to set waypoint at entrance of room
    public SearchMovement(Bounds roomBounds)
    {
        this.roomBounds = roomBounds;
    }
    public void Move(NavMeshAgent agent, Transform enemyTransform, Transform player, EnemyData data)
    {
        agent.speed = data.searchSpeed;
        bool arrived = !agent.pathPending && agent.remainingDistance <= agent.stoppingDistance;

        if (waiting)
        {
            waitTimer += Time.deltaTime;
            if (waitTimer >= waitDuration)
            {
                waiting = false;
                PickNewPoint(agent, data);
            }
            return;
        }
        if (arrived)
        {
            waiting = true;
            waitTimer = 0f;
        }
    }

    private void PickNewPoint(NavMeshAgent agent, EnemyData data)
    {
        Vector3 randomPoint = GetSearchPoint(agent, data.minSearchDistance);
        if (NavMesh.SamplePosition(randomPoint, out NavMeshHit hit, 2f, NavMesh.AllAreas))
        {
            agent.SetDestination(hit.position);
            Debug.Log("Destination set to: " + hit.position);

        }
    }

    private Vector3 GetSearchPoint(NavMeshAgent agent, float minDistance)
    {
        Vector3 candidatePoint;
        int attempts = 0;

        do
        {
            candidatePoint = new Vector3(
                Random.Range(roomBounds.min.x, roomBounds.max.x),
                roomBounds.center.y,
                Random.Range(roomBounds.min.z, roomBounds.max.z)
            );
            attempts++;
        }
        while (Vector3.Distance(agent.transform.position, candidatePoint) < minDistance && attempts < 10);

        return candidatePoint;
    }
}
