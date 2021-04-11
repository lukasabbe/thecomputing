using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoCrafter : MonoBehaviour
{
    //list of matiral
    //*0 = gold
    //*1 = silver
    //*3 = plastic
    //*4 = rubber
    //ID list
    //*1 = gold
    //*2 = silver
    //*5 = platic
    //*7 = rubber

    public GameObject dropLocation, leftInput, rightInput;

    public List<GameObject> outputItems = new List<GameObject>(); // List of all craftable items

    void Update()
    {
        Crafter();
    }
    void Crafter()
    {
        Collider2D[] leftSide = Physics2D.OverlapBoxAll(leftInput.transform.position, new Vector2(0.2f, 0.2f), 0); // Kollar på 1 item på den vänstra sidan
        Collider2D[] rightSide = Physics2D.OverlapBoxAll(rightInput.transform.position, new Vector2(0.2f, 0.2f), 0); // Kollar på 1 item på den högra sidan

        GameObject itemA = null, itemB = null;
        int id1 = 10000, id2 = 10000;

        foreach (Collider2D a in leftSide)
        {
            if (a.gameObject.tag == "Item") // id1 is the id of the last item in the list.
            {
                itemA = a.gameObject;
                id1 = a.gameObject.GetComponent<Item>().id;
            }
        }
        foreach (Collider2D b in rightSide)
        {
            if (b.gameObject.tag == "Item") // id2 is the id of the last item in the list.
            {
                itemB = b.gameObject;
                id2 = b.gameObject.GetComponent<Item>().id;
            }
        }
        if (EmptyInFront())
        {
            // All recipes for crafting
            if (id1 == 1 && id2 == 5 || id2 == 1 && id1 == 5) // 1 = gold, 5 = plastic   output 0 = circuit board
            {
                // Drop wanted item and delete required items.
                GameObject craftedItem = Instantiate(outputItems[0]);
                craftedItem.transform.position = dropLocation.transform.position;
                Destroy(itemA);
                Destroy(itemB);
            }
            // TEST RECIPE för test
            else if (id1 == 7 && id2 == 2 || id2 == 7 && id1 == 2) // 8 = trash plastic, 0 = scrap metal   output 
            {
                // Drop wanted item and delete required items.
                GameObject craftedItem = Instantiate(outputItems[1]);
                craftedItem.transform.position = dropLocation.transform.position;
                Destroy(itemA);
                Destroy(itemB);
            }
            // TEST RECIPE för test
            else if (id1 == 0 && id2 == 8 || id2 == 0 && id1 == 8) // 8 = trash plastic, 0 = scrap metal   output 
            {
                // Drop wanted item and delete required items.
                GameObject craftedItem = Instantiate(outputItems[2]);
                craftedItem.transform.position = dropLocation.transform.position;
                Destroy(itemA);
                Destroy(itemB);
            }
        }
    }
    bool EmptyInFront() // Makes sure it only drops when its empty at the drop location, to avoid many items in the same place
    {
        Collider2D[] hitColliders = Physics2D.OverlapBoxAll(dropLocation.transform.position, new Vector2(0.25f, 0.25f), 0);

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
