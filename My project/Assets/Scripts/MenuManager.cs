using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] UiToggle toggleUI;
    [SerializeField] private GameObject menuCanvas;
    [SerializeField] private GameObject journalCanvas;
    [SerializeField] private GameObject controlCanvas;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        journalCanvas.SetActive(false);
        controlCanvas.SetActive(false);
    }

    void Update()
    {
        if (toggleUI.toggled)
        {
            CloseJournal();
            CloseControls();
        }
    }

    public void OpenJournal()
    {
        journalCanvas.SetActive(true);
        toggleUI.toggled = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = true;
    }

    public void CloseJournal()
    {
        journalCanvas.SetActive(false);
    }

    public void OpenControls()
    {
        controlCanvas.SetActive(true);
        toggleUI.toggled = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = true;
    }

    public void CloseControls()
    {
        controlCanvas.SetActive(false);
    }
}
