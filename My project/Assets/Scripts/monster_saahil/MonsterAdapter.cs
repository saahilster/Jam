using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MonsterAdapter : MonoBehaviour
{
    public UnityEvent TouchedPlayer;
    [SerializeField] List<Transform> tpSpots = new List<Transform>{};

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void TeleportAway()
    {
        if (tpSpots.Count <= 0)
        return;

        int randInt = Random.Range(0, tpSpots.Count - 1);
        gameObject.transform.position = tpSpots[randInt].position;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            TouchedPlayer.Invoke();            
        }
    }
}
