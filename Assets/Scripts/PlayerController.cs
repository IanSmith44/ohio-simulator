using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
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
    private void Start()
    {
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
        if (groundedPlayer && playerVelocity.x == 0 && playerVelocity.y == 0)
        {
            State = playerState.idle;
        }
        else if (!groundedPlayer && !sprintin)
        {
            State = playerState.jumping;
        }
        else if (!groundedPlayer && sprintin)
        {
            State = playerState.sprintJumping;
        }
        else if (movementInput.x == 0 && groundedPlayer)
        {
            State = playerState.idle;
        }
        else if (movementInput.x != 0 && groundedPlayer && !sprintin)
        {
            State = playerState.walking;
        }
        else if (movementInput.x != 0 && groundedPlayer && sprintin)
        {
            State = playerState.running;
        }
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }
        if (sprintin)
        {
            Vector3 move = new Vector2(movementInput.x, 0);
            controller.Move(move * Time.deltaTime * playerSpeed * 4);
        }
        else
        {
            Vector3 move = new Vector2(movementInput.x, 0);
            controller.Move(move * Time.deltaTime * playerSpeed * 2);
        }
        // Changes the height position of the player..
        if (jumped && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }
}
