using UnityEngine;
using UnityEngine.InputSystem;

public class rbPlayerController : MonoBehaviour
{
    [SerializeField] private Transform enemy;
    [SerializeField] private float camDistance = 7.5f;
    [SerializeField] private Camera cam;
    public Rigidbody2D rb;

    public Animator animator;

    public enum PlayerState
    {
        Idle,
        Walking,
        Running,
        Jumping,
        SprintJumping
    }
    public PlayerState State;
    [SerializeField] private float playerSpeed = 2.0f;
    [SerializeField] private float jumpHeight = 1.0f;
    [SerializeField] private bool grounded;
    private Vector2 movementInput = Vector2.zero;
    private bool jumped = false;
    private bool sprintin = false;
    private SpriteRenderer sr;

    private void Start()
    {
        enemy = GameObject.Find("Fire").transform;
        cam = FindObjectOfType<Camera>();;
        sr = GetComponent<SpriteRenderer>();
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
        sprintin = context.action.triggered;
    }

    void Update()
    {
        if (transform.position.x < enemy.position.x)
        {
            Destroy(gameObject);
        }
        if (cam.transform.position.x - transform.position.x > camDistance)
        {
            transform.position = new Vector3(cam.transform.position.x - camDistance + 0.05f, transform.position.y, transform.position.z);
        }
        if (cam.transform.position.x - transform.position.x < -1 * camDistance)
        {
            transform.position = new Vector3(cam.transform.position.x + camDistance - 0.05f, transform.position.y, transform.position.z);
        }
        if (!grounded && sprintin)
        {
            State = PlayerState.SprintJumping;
            animator.SetBool("Run", true);
        }
        else if (!grounded)
        {
            State = PlayerState.Jumping;
            animator.SetBool("Run", true);
        }
        else if (Mathf.Abs(movementInput.x) < 0.1f)
        {
            State = PlayerState.Idle;
            animator.SetBool("Run", false);
        }
        else if (movementInput.x != 0 && grounded && !sprintin)
        {
            State = PlayerState.Walking;
            animator.SetBool("Run", true);
        }
        else if (movementInput.x != 0 && grounded && sprintin)
        {
            State = PlayerState.Running;
            animator.SetBool("Run", true);
        }
        Vector3 move = new Vector2(movementInput.x, 0);
        float speedMultiplier = sprintin ? 4 : 2;
        rb.AddForce(move * Time.deltaTime * playerSpeed * speedMultiplier);
        if (jumped && grounded)
        {
            rb.velocity = new Vector2(rb.velocity.x,jumpHeight);
            //rb.AddForce(Vector3.up * jumpHeight, ForceMode2D.Impulse);
            grounded = false;
        }
        sr.flipX = movementInput.x < 0 ? false : (movementInput.x > 0 ? true : sr.flipX);
    }

    void FixedUpdate()
    {
        if (movementInput == Vector2.zero)
        {
            rb.velocity = new Vector2(rb.velocity.x / 1.1f, rb.velocity.y);
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            grounded = true;
        }
        if (collision.gameObject.tag == "Die")
        {
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "Finish")
        {
            Destroy(gameObject);
            Debug.Log("FINISH!");
        }
        if (collision.gameObject.tag == "Jump Pad")
        {
            rb.velocity = new Vector2(rb.velocity.x, 20);
        }
    }
}
