using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    // Movement speed of the camera
    public float accelerationSpeed = 8;
    // Slows camera movement over time
    public float friction = 8;
    Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        Movement();
        Friction();
    }
    void Movement()
    {
        // Direction axis input
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        // 
        rb.AddForce(transform.right * 1000 * accelerationSpeed * horizontal * Time.deltaTime);
        rb.AddForce(transform.up * 1000 * accelerationSpeed * vertical * Time.deltaTime);
    }
    void Friction()
    {
        rb.velocity = rb.velocity / (friction * Time.deltaTime + 1);
    }
}
