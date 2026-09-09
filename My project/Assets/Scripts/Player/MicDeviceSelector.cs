using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class MicDeviceSelector : MonoBehaviour
{
    [SerializeField] private InputActionReference toggleAction;
    public TMP_Dropdown dropdown;
    public MicListener micListener;
    private string currentDevice;

    private void Start()
    {
        PopulateDropdown();
    }
    private void Update()
    {
        if (toggleAction.action.WasPressedThisFrame())
        {
            gameObject.SetActive(!gameObject.activeSelf);
        }
    }
    private void PopulateDropdown()
    {
        dropdown.ClearOptions();

        if (Microphone.devices.Length == 0)
        {
            Debug.LogWarning("No microphone devices found.");
            return;
        }

        List<string> options = new List<string>(Microphone.devices);
        dropdown.AddOptions(options);

        dropdown.onValueChanged.AddListener(OnDeviceSelected);

        currentDevice = Microphone.devices[0];
        micListener.SetDevice(currentDevice);

    }
    private void OnDeviceSelected(int index)
    {
        string newDevice = Microphone.devices[index];
        if (newDevice == currentDevice) return;
        currentDevice = newDevice;
        GlobalMicDevice.SelectedDevice = currentDevice;
        micListener.SetDevice(currentDevice);
    }
}
