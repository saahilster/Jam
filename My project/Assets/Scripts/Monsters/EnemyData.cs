using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Scriptable Objects/EnemyData")]
public class EnemyData : ScriptableObject
{
    
    public string enemyName;
    public Sprite enemySprite;
    [Header("Movement")]
    public float patrolSpeed;
    public float chaseSpeed;
    public float searchSpeed;
    public MovementType movementType;

    //detection
    [Header("Detection")]
    public float detectionRadius;
    public float fieldOfViewAngle;
    public LayerMask obstacleLayerMask;
    public float lostInterestTime;
    public float chaseLoseDuration;
    public float hearingRadius;
    public float micThreshhold;
    public float searchDuration;
    public int damageToPlayer;
    public DetectionType detectionType;
    public float minSearchDistance;
    //audio
    [Header("Audio")]
    public AudioClip[] footstepSounds;
    public AudioClip detectionSound;
    public AudioClip attackSound;

    //Animation
    [Header("Animation")]
    public Sprite[] walkFrames;
    public Sprite[] chaseFrames;
    public float animationFrameRate;
}

public enum MovementType { Patrol, Chase, Flee, Search}