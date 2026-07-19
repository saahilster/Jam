using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public enum EnemySoundClip
{
    Walking,
    Growl,
    Chase,
    Roar,
    Jumpscare
}

public class EnemyAudioManag : MonoBehaviour
{
    [SerializeField] public AudioSource source;
    [SerializeField] public AudioSource src2;
    [SerializeField] public AudioSource src3;
    [SerializeField] List<AudioClip> clips = new List<AudioClip>{};

    public EnemySoundClip state;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaySound(EnemySoundClip clip, AudioSource src, float vol)
    {
        int val = (int)clip;
        src.clip = clips[val];
        src.volume = vol;
        if (!src.isPlaying)
        {
            Debug.Log(clip + "is playing");
            src.Play();
        }
    }
    public void StopSound(AudioSource src)
    {
        src.Stop();
    }


}
