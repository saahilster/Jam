using Unity.VisualScripting;
using UnityEngine;

public class teleport : MonoBehaviour
{


    [SerializeField] private Transform targetDestination;
    [SerializeField] private SoundManager sm;
    [SerializeField] private Sounds sound;
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
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.transform.position = targetDestination.position;
            sm.PlaySound(SourceDest.MONSTER, sound, 0.8f);
        }
    }
}
