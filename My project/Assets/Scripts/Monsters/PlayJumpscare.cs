using System.Collections;
using UnityEngine;

public class PlayJumpscare : MonoBehaviour
{
    public void TriggerJumpscare(GameObject canvas, EnemyAudioManag audioManag, float volume, float duration)
    {
        StartCoroutine(BeginJumpscare(canvas, audioManag, volume, duration));
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private IEnumerator BeginJumpscare(GameObject canvas, EnemyAudioManag audioManag, float volume, float duration)
    {
        canvas.SetActive(true);
        audioManag.PlaySound(EnemySoundClip.Jumpscare, audioManag.src3 , volume);
        yield return new WaitForSeconds(duration);

        canvas.SetActive(false);
        audioManag.StopSound(audioManag.source);
    }
}
