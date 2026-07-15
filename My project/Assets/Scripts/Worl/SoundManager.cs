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
    MONSTER1_STEPS,
    MONSTER2_STEPS
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

    public void PlaySound(SourceDest srcChannel, Sounds clip)
    {
        int src = (int)srcChannel;
        int val = (int)clip;
        source[src].clip = audioClips[val];
        if (!source[src].isPlaying)
        {
            source[src].Play();
        }

    }
    public void StopSound(SourceDest srcChannel, Sounds clip)
    {
        int src = (int)srcChannel;        
        source[src].Stop();
        source[src].clip = null;
    }
}
