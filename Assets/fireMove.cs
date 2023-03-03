using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireMove : MonoBehaviour
{
    [SerializeField] private float cameraPos = -1.270032f;
    [SerializeField] private Camera cam;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float speed = 2f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (cam.transform.position.x != cameraPos)
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
        }
    }
}
