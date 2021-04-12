using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FilterScript : MonoBehaviour
{
    // For both variables, 0 = left side and 1 = right side.    You can chose on side the item should drop
    public GameObject[] dropLocation; // Where the filtered out item is dropped
    public int filterID = 0; // The id of the item(s) that should be filtered out
    public int dropSide = 0; // if 0, it will filter out to the left side, 1 to the right

    void Update()
    {
        Filter();
    }
    private void Filter()
    {
        Collider2D[] checkItems = Physics2D.OverlapBoxAll(transform.position, new Vector2(0.05f, 0.05f), 0); // Looks at all the items on top of the building

        foreach (Collider2D item in checkItems)
        {
            if (item.gameObject.tag == "Item")
            {
                if (item.gameObject.GetComponent<Item>().id == filterID && EmptyInFront()) // If its the right item
                {
                    item.gameObject.transform.position = dropLocation[dropSide].transform.position; // Move it to the left/right side
                }
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
    }
}
