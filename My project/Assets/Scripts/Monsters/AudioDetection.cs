using UnityEngine;

public class AudioDetection : DetectionBehavior
{
    public bool CanDetectPlayer(EnemyController enemy)
    {

        return CanHearPlayerNosie(enemy);
    }
    public bool CanHearMic(EnemyController enemy)
    {
        return enemy.Mic.GetMicVolume() > enemy.Data.micThreshhold;
    }

    public bool CanHearPlayerNosie(EnemyController enemy)
    {
        if (PlayerNoise.Instance == null){
            Debug.LogWarning("Playernoise Instance null");
            return false;
        }
        float noiseLevel = PlayerNoise.Instance.GetNoiseLevel();
        if (noiseLevel <= enemy.Data.micThreshhold) return false;
        float distance = Vector3.Distance(enemy.transform.position, enemy.Player.position);
        return distance <= enemy.Data.hearingRadius;
    }
    public State GetReactionState() => State.Search;
}
