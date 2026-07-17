using System.Collections;
using System.Diagnostics.CodeAnalysis;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private PlayerAudioManager pam;
    [SerializeField] private Animator anim;
    private bool sprintCalled;
    public int hungerBar = 5;

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
    IEnumerator DepleteStamina()
    {
        while (Keyboard.current.shiftKey.isPressed && hungerBar > 0)
        {
            hungerBar--;
            yield return new WaitForSeconds(1);
        }

        sprintCalled = false;
    }

    private void Update()
    {
        // Read raw input each frame
        moveInput = moveAction.ReadValue<Vector2>();
        

        if(Keyboard.current.shiftKey.isPressed && hungerBar > 0 && moveInput != Vector2.zero)
        {
            anim.SetBool("walking", false);
            pam.PlaySound(SoundClip.RUNNING);
            moveSpeed = 8f;

            if (!sprintCalled)
            {
                sprintCalled = true;
                StartCoroutine(DepleteStamina());
            }
        }
        else if (moveInput != Vector2.zero)
        {
            moveSpeed = 2.8f;
            pam.PlaySound(SoundClip.WALKING);
            anim.SetBool("walking", true);
        }
        else
        {
            moveSpeed = 2.8f;
            pam.StopSound(SoundClip.WALKING);
            anim.SetBool("walking", false);
        }


        if(moveSpeed > 6)
        {
            Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, 60, 3 * Time.deltaTime);
            anim.SetBool("walking", false);
            anim.SetBool("running", true);
        }
        else
        {
            Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, 40, 3 * Time.deltaTime);
            anim.SetBool("running", false);
        }

    }
    private void FixedUpdate()
    {
        Vector3 direction = (transform.forward * moveInput.y) + (transform.right * moveInput.x);
        Vector3 velocity = direction * moveSpeed;
        rb.linearVelocity = new Vector3(velocity.x, rb.linearVelocity.y, velocity.z);
    }
}
