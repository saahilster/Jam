using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public enum EnemySoundClip
{
    Walking,
    Roar,
    Crying,
    Music
}

public class EnemyAudioManag : MonoBehaviour
{
    [SerializeField] public AudioSource source;
    [SerializeField] public AudioSource src2;
    [SerializeField] List<AudioClip> clips = new List<AudioClip>{};

    public SoundClip state;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaySound(SoundClip clip, AudioSource src, float vol)
    {
        int val = (int)clip;
        src.clip = clips[val];
        src.volume = vol;
        if (!source.isPlaying)
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
