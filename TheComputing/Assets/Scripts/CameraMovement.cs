using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    // Movement speed of the camera
    public float accelerationSpeed = 8;
    // Slows camera movement over time
    public float friction = 8;
    Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        Movement();
        Friction();
        Zoom();
    }
    void Movement()
    {
        // Direction axis input
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        // Adds the movement force
        rb.AddForce(transform.right * 1000 * accelerationSpeed * horizontal * Time.deltaTime);
        rb.AddForce(transform.up * 1000 * accelerationSpeed * vertical * Time.deltaTime);
    }
    void Friction()
    {
        rb.velocity = rb.velocity / (friction * Time.deltaTime + 1);
    }
    void Zoom()
    {
        Camera cam = GetComponent<Camera>();

        // Scrolling to zoom in/out
        if (Input.GetAxis("Mouse ScrollWheel") < 0f) // Scroll down
        {
            //     Jag tror pixel perfect camera skapar problem när man vill zooma ut/in, inte säker på hur man fixar det.

            //cam.orthographicSize = 10;
        }
        else if (Input.GetAxis("Mouse ScrollWheel") > 0f) // Scroll up
        {
        }
    }
}