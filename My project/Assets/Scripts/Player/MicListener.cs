using System.Collections.Generic;
using UnityEngine;

public class MicListener : MonoBehaviour
{
    [Header("Mic Settings")]
    [SerializeField] private int sampleWindow = 256;
    [SerializeField] private int frequency = 16000;
    [SerializeField] private int lengthSec = 1;
    [SerializeField] private float micSensitivity = 1f;
    private AudioClip micClip;
    private string micDevice;
    private float currentVolume;
    private bool isRecording;

    public float GetMicVolume() => currentVolume;
    
    public bool IsRecording() => isRecording;

    private void Start()
    {
        SetDevice(GlobalMicDevice.SelectedDevice);
    }
    private void Update()
    {
        if (!isRecording || micClip == null) return;
        currentVolume = CalculateVolume();
        PlayerNoise.Instance.AddNoise(currentVolume * micSensitivity);
    }

    public void SetDevice(string deviceName)
    {
        if (!string.IsNullOrEmpty(micDevice) && Microphone.IsRecording(micDevice))
        {
            Microphone.End(micDevice);
        }

        micDevice = deviceName;
        micClip = Microphone.Start(micDevice, true, lengthSec, frequency);
        isRecording = true;
    }

    public void StopListening()
    {
        if (!string.IsNullOrEmpty(micDevice) && Microphone.IsRecording(micDevice))
        {
            Microphone.End(micDevice);
        }
        isRecording = false;
    }

    private float CalculateVolume()
    {
        int micPosition = Microphone.GetPosition(micDevice);
        int offset = micPosition - sampleWindow;
        if (offset < 0) return currentVolume;
        float[] samples = new float[sampleWindow];
        micClip.GetData(samples, offset);

        float sum = 0f;
        for (int i = 0; i < samples.Length; i++)
        {
            sum += samples[i] * samples[i];
        }
        return Mathf.Sqrt(sum / samples.Length);
    }

    private void OnDestroy()
    {
        StopListening();
    }
}
