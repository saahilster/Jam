using UnityEngine;
using UnityEngine.InputSystem;

public class UiToggle : MonoBehaviour
{
    [SerializeField] private GameObject JournalCanvas;
    private InputSystem_Actions input;
    private InputAction menuToggle;
    public bool toggled;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        JournalCanvas.SetActive(false);
        input = new InputSystem_Actions();
        Cursor.visible = toggled;
        Cursor.lockState = CursorLockMode.None;
    }

    void OnEnable()
    {
        menuToggle = input.Player.OpenMenu;
        menuToggle.Enable();
        menuToggle.performed += ToggleMenu;
    }
    void OnDisable()
    {
        menuToggle.Disable();
        menuToggle.performed -= ToggleMenu;
    }

    private void ToggleMenu(InputAction.CallbackContext context)
    {
        toggled = !toggled;
        JournalCanvas.SetActive(toggled);
        Cursor.visible = toggled;

        if (toggled)
        {
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
