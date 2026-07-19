using UnityEngine;
using UnityEngine.AI;

public class EnemySpriteAnimator : MonoBehaviour
{
    [SerializeField] NavMeshAgent agent;
    private EnemyData enemyData;
    private SpriteRenderer spriteRenderer;
    private State currentState;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Initialize(EnemyData data)
    {
        enemyData = data;
    }

    public void SetState(State newState)
    {
        currentState = newState;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyData == null || spriteRenderer == null)
            return;
        if (currentState == State.Chase)
        {
            int frameIndex = Mathf.FloorToInt(Time.time / enemyData.animationFrameRate) % enemyData.chaseFrames.Length;
            spriteRenderer.sprite = enemyData.chaseFrames[frameIndex];
        }
        if (currentState == State.Patrol || currentState == State.Search)
        {
            if (agent.velocity.magnitude != 0f)
            {
                int frameWalkIndex = Mathf.FloorToInt(Time.time / enemyData.animationWalkFrameRate) % enemyData.walkFrames.Length;
                spriteRenderer.sprite = enemyData.walkFrames[frameWalkIndex];
            }
        }
    }
}

