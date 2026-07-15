using Unity.VisualScripting;
using UnityEngine;

public class SoundTriggerScript : MonoBehaviour
{
    [SerializeField] SoundManager sm;
    [SerializeField] Sounds clip;
    [SerializeField] int probability;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Debug.Log("Player found");
            int ranInt = Random.Range(1, 10);
            if (ranInt >= probability)
            {
                Debug.Log("sound success");
                sm.PlaySound(SourceDest.MONSTER, clip, 0.6f);
            }
        }
    }
}
