using UnityEngine;

public class MenuManager : MonoBehaviour
{
    bool usingUI = false;
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
            
            if(!usingUI)
        {
            Debug.Log("Worked");
            Cursor.lockState = CursorLockMode.Locked;
        }
        }
        
    }

    public void OpenJournal()
    {
        usingUI = true;
        journalCanvas.SetActive(true);
        toggleUI.toggled = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void CloseJournal()
    {
        usingUI = false;
        journalCanvas.SetActive(false);
        toggleUI.toggled = true;
    }

    public void OpenControls()
    {
        usingUI = true;
        controlCanvas.SetActive(true);
        toggleUI.toggled = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void CloseControls()
    {
        usingUI = false;
        controlCanvas.SetActive(false);
        toggleUI.toggled = true;
    }
}
