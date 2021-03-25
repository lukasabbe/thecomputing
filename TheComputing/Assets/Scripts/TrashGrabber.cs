using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashGrabber : MonoBehaviour
{
    public GameObject dropLocation, trashItem;
    float dropCooldown = 0; // Cooldown in seconds.
    public int speedLVL = 0;

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
            float waitTime = 3; // Default drop waiting time at lvl 0
            if (dropCooldown < waitTime) dropCooldown = dropCooldown + (1 + speedLVL / 1.5f) * Time.deltaTime;
            if (dropCooldown >= waitTime)
            {
                GameObject trash;
                trash = Instantiate(trashItem);
                trash.transform.position = dropLocation.transform.position;
                trash.transform.Rotate(0f, 0f, Random.Range(0.0f, 360.0f));
                dropCooldown = 0;
            }
        }
    }
}
