using UnityEngine;
using UnityEngine.AI;

public class ChildBehavior : MonoBehaviour
{
    [SerializeField] private Transform waypointsParent;
    [SerializeField] private EnemyData data;
    private NavMeshAgent agent;
    private Transform[] waypoints;
    private float lookTimer;
    private float lookDuration = 4f;
    private State currentState;
    private int index = 0;
    private enum State {Flee, Waiting, Dead};


    /// <summary>
    /// Get waypoints, upon looking inflict affect on player and flee to a specific location and wait
    /// </summary>
    /// 
    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        waypoints = new Transform[waypointsParent.childCount];
        for (int i = 0; i < waypointsParent.childCount; i++)
        {
            waypoints[i] = waypointsParent.GetChild(i);
        }
        agent.speed = data.patrolSpeed;
        currentState = State.Flee;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case State.Waiting:
                if (IsBeingObserved())
                {
                    lookTimer += Time.deltaTime;
                }
                else
                {
                    lookTimer = 0f;
                }
                if (lookTimer >= lookDuration)
                {
                    //Insert trigger effect
                    TriggerEffect();
                    currentState = State.Flee;
                    lookTimer = 0f;
                }
                break;
            case State.Flee:
                if (!agent.hasPath)
                {
                agent.SetDestination(waypoints[index].position);
                }
                if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
                {
                    index = (index + 1) % waypoints.Length;
                    currentState = State.Waiting;
                }
                break;

        }
    }

    private bool IsBeingObserved()
    {
        return true;
    }
    private void TriggerEffect()
    {
        //Insert Effects
    }
}
