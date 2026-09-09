using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private PlayerAudioManager pam;
    [SerializeField] private Animator anim;
    private bool breatheCall;
    private bool sprintCalled;
    public int hungerBar = 5;
    [SerializeField] TextMeshProUGUI staminaDisplay;

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
            yield return new WaitForSeconds(2.25f);
        }

        sprintCalled = false;
    }

    private void MovementState()
    {
        if(Keyboard.current.shiftKey.isPressed && hungerBar > 0 && moveInput != Vector2.zero)
        {
            anim.SetBool("walking", false);
            pam.PlaySound(SoundClip.RUNNING, pam.source, 0.4f);
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
            pam.PlaySound(SoundClip.WALKING, pam.source, 0.8f);
            anim.SetBool("walking", true);
        }
        else
        {
            moveSpeed = 2.8f;
            pam.StopSound(pam.source);
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

    private void Update()
    {
        staminaDisplay.text = "Stamina: " + hungerBar.ToString();
        moveInput = moveAction.ReadValue<Vector2>();
        MovementState();
        if(hungerBar == 0 )
        {
            pam.PlaySound(SoundClip.BREATHING, pam.src2, 0.35f);
        }
        // else
        // {
        //     pam.StopSound(pam.src2);
        // }
    }
    private void FixedUpdate()
    {
        Vector3 direction = (transform.forward * moveInput.y) + (transform.right * moveInput.x);
        Vector3 velocity = direction * moveSpeed;
        rb.linearVelocity = new Vector3(velocity.x, rb.linearVelocity.y, velocity.z);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Snack") && hungerBar < 5)
        {
            Debug.Log("I ate the bar");
            hungerBar += 2;
        }
    }
}
