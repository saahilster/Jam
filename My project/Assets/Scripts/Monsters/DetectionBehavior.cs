using UnityEngine;

public interface DetectionBehavior
{
    bool CanDetectPlayer(EnemyController enemy);
    State GetReactionState();
}
