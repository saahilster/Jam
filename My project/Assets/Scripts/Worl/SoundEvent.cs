using UnityEngine;

public class SoundEvent : MonoBehaviour
{
    [SerializeField] SoundManager soundManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        BackGroundMusic();
    }

    public void BackGroundMusic()
    {
        soundManager.PlaySound(SourceDest.MUSIC, Sounds.CAVE_THEME, 0.7f);
    }
}
