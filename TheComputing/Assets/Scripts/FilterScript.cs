using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FilterScript : MonoBehaviour
{
    // For both variables, 0 = left side and 1 = right side.    You can chose on side the item should drop
    public GameObject[] dropLocation; // Where the filtered out item is dropped
    public int filterID = 0; // The id of the item(s) that should be filtered out
    public int dropSide = 0; // if 0, it will filter out to the left side, 1 to the right

    // Update is called once per frame
    void Update()
    {
        
    }/*
    public void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "Item" && col.gameObject.GetComponent<Item>() != null) // When an item rolls over the filter
        {
            if (col.gameObject.GetComponent<Item>().id == filterID) // If its the right item
            {
                col.gameObject.transform.position = dropLocation[dropSide].transform.position;
            }
        }
    }
    bool EmptyInFront() // Makes sure it only drops when its empty at the drop location, to avoid many items in the same place
    {
        Collider2D[] hitColliders = Physics2D.OverlapBoxAll(dropLocation[dropSide].transform.position, new Vector2(0.25f, 0.25f), 0);

        if (hitColliders == null) return false;
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.gameObject.tag == "Item")
            {
                return false;
            }
        }
        return true;
    }*/
}
