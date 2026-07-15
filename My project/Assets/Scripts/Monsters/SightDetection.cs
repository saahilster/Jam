using UnityEngine;

public class SightDetection : DetectionBehavior
{
    public bool CanDetectPlayer(EnemyController enemy)
    {
        float distance = Vector3.Distance(enemy.transform.position, enemy.Player.position);
        if (distance > enemy.Data.detectionRadius)
            return false;

        Vector3 directionToPlayer = (enemy.Player.position - enemy.transform.position).normalized;
        float angle = Vector3.Angle(enemy.transform.forward, directionToPlayer);
        if (angle > enemy.Data.fieldOfViewAngle * 0.5f)
            return false;

        if (Physics.Raycast(enemy.transform.position, directionToPlayer, out RaycastHit hit, distance, enemy.Data.obstacleLayerMask))
        {
            return false;
        }
        return true;
    }
    public State GetReactionState() => State.Chase;
}
