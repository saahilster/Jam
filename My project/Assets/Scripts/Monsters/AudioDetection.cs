using UnityEngine;

public class AudioDetection : DetectionBehavior
{
    public bool CanDetectPlayer(EnemyController enemy)
    {
        return CanHearMic(enemy) || CanHearPlayerNosie(enemy);
    }
    public bool CanHearMic(EnemyController enemy)
    {
        return enemy.Mic.GetMicVolume() > enemy.Data.micThreshhold;
    }

    public bool CanHearPlayerNosie(EnemyController enemy)
    {
        float distance = Vector3.Distance(enemy.transform.position, enemy.Player.position);
        return distance <= enemy.Data.hearingRadius * PlayerNoise.Instance.GetNoiseLevel();
    }
    public State GetReactionState() => State.Search;
}
