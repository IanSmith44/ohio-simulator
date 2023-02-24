using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireMove : MonoBehaviour
{
    [SerializeField] private Vector3 cameraPos;
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
        if (cam.transform.position != cameraPos)
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
        }
    }
}
