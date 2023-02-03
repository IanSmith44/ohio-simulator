using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    public float moveSpeedx = 8f;
    public float moveSpeedy = 5f;
    public Rigidbody2D rb;
    private Vector2 movementx;
    private Vector2 movementy;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        movementx.x = Input.GetAxisRaw("Horizontal");
        movementy.y = Input.GetAxisRaw("Vertical");

    }
    void FixedUpdate()
    {
        rb.AddForce(movementx * moveSpeedx);
        rb.AddForce(movementy * moveSpeedy);
    }
}
