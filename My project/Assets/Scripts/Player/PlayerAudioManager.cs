using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public enum SoundClip
{
    WALKING,
    RUNNING,
    BREATHING,
    HEARTBEAT
}

public class PlayerAudioManager : MonoBehaviour
{
    [SerializeField] AudioSource source;
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

    public void PlaySound(SoundClip clip)
    {
        int val = (int)clip;
        source.clip = clips[val];
        if (!source.isPlaying)
        {
            source.Play();
        }
    }
    public void StopSound(SoundClip clip)
    {
        int val = (int)clip;
        source.Stop();
    }


}
