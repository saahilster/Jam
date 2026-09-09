using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public enum SourceDest
{
    MUSIC,
    MONSTER,
    CATACOMB
}

public enum Sounds
{
    CAVE_THEME,

    //these are also set to random for SoundOfFootsteps (1-4)
    MONSTER1_STEPS,
    MONSTER2_STEPS,
    THUMP1,
    THUMP2,
    TP,
    LAUGHTER
}
public class SoundManager : MonoBehaviour
{
    [SerializeField] List<AudioSource> source = new List<AudioSource> { };
    [SerializeField] List<AudioClip> audioClips = new List<AudioClip> { };
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlaySound(SourceDest srcChannel, Sounds clip, float volume)
    {
        int src = (int)srcChannel;
        int val = (int)clip;
        source[src].clip = audioClips[val];
        source[src].volume = volume;
        if (!source[src].isPlaying)
        {
            source[src].Play();
        }

    }
    public void StopSound(SourceDest srcChannel)
    {
        int src = (int)srcChannel;        
        source[src].Stop();
        source[src].clip = null;
    }

    public void DirectPlay(AudioSource src, AudioClip clip, float vol)
    {
        src.clip = clip;
        src.volume = vol;
        if (!src.isPlaying)
        {
            src.Play();
        }
    }
    public void DirectStop(AudioSource src)
    {
        src.Stop();
    }
}
