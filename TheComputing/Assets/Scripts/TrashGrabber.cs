using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashGrabber : MonoBehaviour
{
    public GameObject dropLocation, trashItem;
    public float dropCooldown = 0; // Cooldown in seconds.

    const int Up = 0, Down = 2, Right = 1, Left = 3;
    void Start()
    {

    }
    void Update()
    {
        
    }
    public void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "Trash Area") // The trash grabber will only drop trash when it sits on a trash area
        {
            if (dropCooldown < 1) dropCooldown = dropCooldown + 1 * Time.deltaTime;
            if (dropCooldown >= 1)
            {
                GameObject trash;
                trash = Instantiate(trashItem);
                trash.transform.position = dropLocation.transform.position;
                dropCooldown = 0;
            }
        }
    }
}
