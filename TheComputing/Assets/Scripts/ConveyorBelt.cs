using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBelt : MonoBehaviour
{
    int direction = 0;
    void Start()
    {
        
    }
    /*
    void Update()
    {
        Collider2D[] hitColliders = Physics2D.(BuildPosition(), 0.1f); // Creates a list of colliders in the tile the cursor is over

        foreach (var hitCollider in hitColliders)
        { // Destroys the gameObject
            Destroy((hitCollider).gameObject);
        }
    }
    */
    public void OnTriggerStay2D(Collider2D col)
    {
        Debug.Log(col.gameObject.name);
        //col.transform.position.y += 0.2f;
    }
    /*
    void OnTriggerStay2D(Collider2D col)
    {
        Debug.Log("GameObject2 collided with " + col.name);
        //other.attachedRigidbody.AddForce(-0.1F * other.attachedRigidbody.velocity);
    }
    */
    
}
