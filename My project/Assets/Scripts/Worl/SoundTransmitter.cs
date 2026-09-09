using UnityEngine;

public class SoundTransmitter : MonoBehaviour
{
    [SerializeField] SoundManager sm;
    [SerializeField] AudioClip clip;
    [SerializeField] AudioSource src;
    [SerializeField] float volume;
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
        if (other.gameObject.CompareTag("Player"))
        {
            int ranInt = Random.Range(1, 10);
            if (ranInt >= probability)
            {
                Debug.Log("sound success");
               PlayIt();
            }
        }
    }

    public void PlayIt()
    {
        sm.DirectPlay(src, clip, volume);
    }
}
