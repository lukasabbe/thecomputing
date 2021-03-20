using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBelt : MonoBehaviour
{
    public int direction = 0;
    void Start()
    {
        SetRotation();
    }
    void SetRotation()
    {
    }
    public void OnTriggerStay2D(Collider2D col)
    {
        Debug.Log(col.gameObject.name);
        col.transform.position = new Vector2(0, col.transform.position.y + 1 * Time.deltaTime);
    }
    /*
    void OnTriggerStay2D(Collider2D col)
    {
        Debug.Log("GameObject2 collided with " + col.name);
        //other.attachedRigidbody.AddForce(-0.1F * other.attachedRigidbody.velocity);
    }
    */
}