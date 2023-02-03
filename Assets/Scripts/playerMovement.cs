using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    public float moveSpeedx = 8f;
    public float moveSpeedy = 5f;
    public float jumpStrength = 10f;
    private bool grounded = true;
    public Rigidbody rb;
    private Vector2 movementx;
    private Vector3 movementy;
    public enum playerState
    {
        Still,
        Moving,
        Slowing
    }
    public playerState State;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        movementx.x = Input.GetAxisRaw("Horizontal");
        movementy.z = Input.GetAxisRaw("Vertical");
        if (movementy.z == 0 && movementx.x == 0 && (rb.velocity.x > 0 || rb.velocity.x < 0 || rb.velocity.y > 0 || rb.velocity.y < 0 || rb.velocity.z > 0 || rb.velocity.z < 0))
        {
            State = playerState.Slowing;
        }
        else if (rb.velocity == new Vector3(0, 0, 0))
        {
            State = playerState.Still;
        }
        else
        {
            State = playerState.Moving;
        }
        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }
    }
    void FixedUpdate()
    {
        rb.AddForce(movementx * moveSpeedx);
        rb.AddForce(movementy * moveSpeedy);
    }
    void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == "Ground")
        {
            //Debug.Log("Grounded");
            grounded = true;
        }
    }
    void Jump()
    {

        if (grounded)
        {
            grounded = false;
            rb.AddForce(new Vector3(0, jumpStrength, 0), ForceMode.Impulse);
        }
    }
}
