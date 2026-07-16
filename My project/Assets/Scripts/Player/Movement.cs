using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private PlayerAudioManager pam;
    [SerializeField] private Animator anim;

    [Header("Input")]
    private InputSystem_Actions input;
    private InputAction moveAction;
    private Rigidbody rb;
    private Vector2 moveInput;

    private void Awake()
    {
        input = new InputSystem_Actions();
        rb = GetComponent<Rigidbody>();
        Cursor.visible = false;
    }

    private void OnEnable()
    {
        moveAction = input.Player.Move;
        moveAction.Enable();
    }

    private void OnDisable()
    {
        moveAction.Disable();
    }

    private void Update()
    {
        // Read raw input each frame
        moveInput = moveAction.ReadValue<Vector2>();
        if (moveInput != Vector2.zero)
        {
            pam.PlaySound(SoundClip.WALKING);
            anim.SetBool("walking", true);
        }
        else
        {
            pam.StopSound(SoundClip.WALKING);
            anim.SetBool("walking", false);
            
        }
        
    }

    private void FixedUpdate()
    {
        Vector3 direction = (transform.forward * moveInput.y) + (transform.right * moveInput.x);
        Vector3 velocity = direction * moveSpeed;
        rb.linearVelocity = new Vector3(velocity.x, rb.linearVelocity.y, velocity.z);
    }
}
