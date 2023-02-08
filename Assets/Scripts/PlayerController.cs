using UnityEngine;
using UnityEngine.InputSystem;

//[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    public Rigidbody rb;
    public Animator animator;
    public enum playerState
    {
        idle,
        walking,
        running,
        jumping,
        sprintJumping
    }
    public playerState State;
    [SerializeField]
    private float playerSpeed = 2.0f;
    [SerializeField]
    private float jumpHeight = 1.0f;
    [SerializeField]
    private float gravityValue = -9.81f;
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private Vector2 movementInput = Vector2.zero;
    private bool jumped = false;
    private bool sprintin = false;
    private SpriteRenderer sr;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        
        controller = gameObject.GetComponent<CharacterController>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
    }
    public void OnJump(InputAction.CallbackContext context)
    {
        jumped = context.action.triggered;
    }
    public void OnSprint(InputAction.CallbackContext context)
    {
        if (context.action.triggered)
        {
            sprintin = true;
        }
        else if (!context.action.triggered)
        {
            sprintin = false;
        }
    }

    void Update()
    {
        groundedPlayer = controller.isGrounded;
        if (movementInput.x == 0 && groundedPlayer)
        {
            State = playerState.idle;
            animator.SetBool("Run", false);
        }
        else if (!groundedPlayer && !sprintin)
        {
            State = playerState.jumping;
            animator.SetBool("Run", true);
        }
        else if (!groundedPlayer && sprintin)
        {
            State = playerState.sprintJumping;
            animator.SetBool("Run", true);
        }
        else if (movementInput.x != 0 && groundedPlayer && !sprintin)
        {
            State = playerState.walking;
            animator.SetBool("Run", true);
        }
        else if (movementInput.x != 0 && groundedPlayer && sprintin)
        {
            State = playerState.running;
            animator.SetBool("Run", true);
        }
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }
        if (sprintin)
        {
            Vector3 move = new Vector2(movementInput.x, 0);
            controller.Move(move * Time.deltaTime * playerSpeed * 4);
            //rb.AddForce(move * Time.deltaTime * playerSpeed * 4);
        }
        else
        {
            Vector3 move = new Vector2(movementInput.x, 0);
            controller.Move(move * Time.deltaTime * playerSpeed * 2);
            //rb.AddForce(move * Time.deltaTime * playerSpeed *2);
        }
        // Changes the height position of the player..
        if (jumped && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }
        if (movementInput.x < 0)
        {
            sr.flipX = false;
        }
        else if (movementInput.x > 0)
        {
            sr.flipX = true;
        }
        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
        //rb.AddForce(playerVelocity * Time.deltaTime);
    }
}
