using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    Rigidbody rb;
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        if (rb.velocity.x > 0.01f || rb.velocity.y > 0.01f || rb.velocity.z > 0.01f) Friction();
        else rb.velocity = rb.velocity * 0;
    }
    void Friction()
    {
        rb.velocity = rb.velocity - (rb.velocity * 4 * Time.deltaTime);
    }
}
