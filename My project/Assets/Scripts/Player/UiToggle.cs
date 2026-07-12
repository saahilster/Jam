using UnityEngine;
using UnityEngine.InputSystem;

public class UiToggle : MonoBehaviour
{
    [SerializeField] private GameObject menuCanvas;
    private InputSystem_Actions input;
    private InputAction menuToggle;
    public bool toggled;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        menuCanvas.SetActive(false);
        input = new InputSystem_Actions();

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
        Debug.Log(toggled);
    }

    void Update()
    {
        if (toggled)
        {   menuCanvas.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            menuCanvas.SetActive(false);
        }

    }

}
