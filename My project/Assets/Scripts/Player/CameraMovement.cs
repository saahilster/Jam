using UnityEngine;
using UnityEngine.InputSystem;

public class CameraMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform playerBody; // the character root (rotates left/right)
    [SerializeField] private Transform cameraTransform; // the camera itself (rotates up/down)

    [Header("Input")]
    private InputSystem_Actions input;
    private InputAction lookAction;
    [Header("Look Settings")]
    [SerializeField] private float mouseSensitivity = 0.025f;
    [SerializeField] private float minPitch = -80f;
    [SerializeField] private float maxPitch = 80f;
    [SerializeField] private bool lockCursor = true;

    private Vector2 lookInput;
    private float pitch = 0f; // vertical rotation accumulator

    private void Awake()
    {
        input = new InputSystem_Actions();
        if (lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    private void OnEnable()
    {
        lookAction = input.Player.Look;
        lookAction.Enable();
    }

    private void OnDisable()
    {
        lookAction.Disable();
    }

    private void Update()
    {
        lookInput = lookAction.ReadValue<Vector2>();

        float mouseX = lookInput.x * mouseSensitivity;
        float mouseY = lookInput.y * mouseSensitivity;

        // Horizontal look: rotate the whole player body around the Y axis
        playerBody.Rotate(Vector3.up * mouseX);

        // Vertical look: rotate only the camera around the X axis, clamped
        pitch -= mouseY;
        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);
        cameraTransform.localRotation = Quaternion.Euler(pitch, 0f, 0f);
    }
}