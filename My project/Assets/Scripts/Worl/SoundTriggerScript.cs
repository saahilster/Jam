using Unity.VisualScripting;
using UnityEngine;

public class SoundTriggerScript : MonoBehaviour
{
    [SerializeField] SoundManager sm;
    [SerializeField] Sounds clip;
    [SerializeField] int probability;

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            int ranInt = Random.Range(1, 10);
            if (ranInt >= probability)
            {
                sm.PlaySound(SourceDest.MONSTER, clip, 0.6f);
            }
        }
    }
}
