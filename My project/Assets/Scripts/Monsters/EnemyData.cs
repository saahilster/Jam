using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Scriptable Objects/EnemyData")]
public class EnemyData : ScriptableObject
{
    public string enemyName;
    public Sprite enemySprite;
    public float patrolSpeed;
    public float chaseSpeed;
    public MovementType movementType;

    //detection
    public float detectionRadius;
    public float fieldOfViewAngle;
    public LayerMask obstacleLayerMask;
    public float lostInterestTime;
    public float hearingRadius;
    public float micThreshhold;
    public float searchDuration;
    
    public int damageToPlayer;
    //audio
    public AudioClip[] footstepSounds;
    public AudioClip detectionSound;
    public AudioClip attackSound;

    //Animation
    public Sprite[] walkFrames;
    public Sprite[] chaseFrames;
    public float animationFrameRate;
}

public enum MovementType { Patrol, Chase, Flee, Search}