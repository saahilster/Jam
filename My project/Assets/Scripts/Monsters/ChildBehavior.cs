using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChildBehavior : MonoBehaviour
{
    [SerializeField] private Transform playerCam;
    [SerializeField] private Transform waypointsParent;
    [SerializeField] private EnemyData data;
    [SerializeField] private float viewDistance = 6f;
    [SerializeField] private LayerMask obstructionMask;
    [SerializeField] private float viewAngle = 30f;
    [SerializeField] private EnemyAudioManag audioManag;
    [SerializeField] List<GameObject> jumpscareCanvas = new List<GameObject>{};
    [SerializeField] private EnemyController mother;
    private PlayJumpscare jumpscare;
    private int jumpscareCounter = 0;

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
        jumpscare = GetComponent<PlayJumpscare>();
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
                    lookTimer = 0f;
                    currentState = State.Flee;
                }
                Debug.Log(currentState);
                break;
            case State.Flee:
                audioManag.PlaySound(EnemySoundClip.Chase, audioManag.source, 0.6f);
                if (!agent.hasPath)
                {
                    //remove
                    bool success = agent.SetDestination(waypoints[index].position);
                    Debug.Log($"SetDestination success={success}, isOnNavMesh={agent.isOnNavMesh}, target={waypoints[index].position}");
                    agent.SetDestination(waypoints[index].position);
                }
                Debug.Log($"remaining={agent.remainingDistance}, stopping={agent.stoppingDistance}, status={agent.pathStatus}, pending={agent.pathPending}, speed={agent.speed}, isStopped={agent.isStopped}");
                if (agent.hasPath && !agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
                {
                    index = (index + 1) % waypoints.Length;
                    currentState = State.Waiting;
                }
                Debug.Log(currentState);
                break;

        }
    }

    private bool IsBeingObserved()
    {
        Vector3 dirToChild = transform.position - playerCam.position;
        float distance = dirToChild.magnitude;
        if (distance <= 3.5f) return true;

        if (distance > viewDistance) return false;

        float angle = Vector3.Angle(playerCam.forward, dirToChild);
        if (angle > viewAngle * 0.5f) return false;

        if (Physics.Raycast(playerCam.position, dirToChild.normalized, out RaycastHit hit, distance, obstructionMask))
        {
            
            return false;
        }
        Debug.Log("Being Looked at");
        return true;
    }
    private void TriggerEffect()
    {
        //Insert Effects
        if (jumpscareCounter < 3)
        {
            //currentState = State.Flee;
            jumpscare.TriggerJumpscare(jumpscareCanvas[0], audioManag, data.jumpscareVolume, data.jumpscareDuration);
            jumpscareCounter += 1;
        }
        else
        {
            jumpscare.TriggerJumpscare(jumpscareCanvas[1], audioManag, data.jumpscareVolume, data.jumpscareDuration);
            data.enemySprite = data.walkFrames[0];
            GetComponent<ChildBehavior>().enabled = false;
        }
        if (jumpscareCounter == 1)
        {
            mother.Activate();
        }
    }
}   
