using UnityEngine;
using UnityEngine.AI;

public interface MovementBehavior
{
    void Move(NavMeshAgent agent, Transform enemyTransform, Transform player, EnemyData data);
}
