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


    public GameObject outputItem; // Output item is the item you want to craft, input items are the items required to craft
    public GameObject dropLocation;
    List<int> recipeID = new List<int>();
    void Start()
    {
        Item outputItemScript = outputItem.GetComponent<Item>();

        if (outputItemScript.id == 9) // Recipe for Circuit Board
        { // Recipe
            recipeID.Add(1); // trash plastic
            recipeID.Add(5); // scrap metal
        }
    }
    void Update()
    {
        Crafter();
    }
    void Crafter()
    {
        Collider2D[] hitColliders = Physics2D.OverlapBoxAll(transform.position, new Vector2(0.498f, 0.498f), 0); // Creates a list of colliders in the tile the cursor is over.
        
        //List<GameObject> ITEM = new List<GameObject>();
        GameObject ITEM1 = null, ITEM2 = null;

        foreach (Collider2D hitCollider in hitColliders)
        {
            if (hitCollider.gameObject.tag == "Item") // Checks if the required items are in the collider.
            {
                int itemID = hitCollider.gameObject.GetComponent<Item>().id;

                if (itemID == recipeID[0]) ITEM1 = (hitCollider).gameObject;
                if (itemID == recipeID[1]) ITEM2 = (hitCollider).gameObject;

                // If all required materials are found, craft item.
                if (ITEM1 != null && ITEM2 != null)
                {   
                    if (EmptyInFront())
                    {
                        // Drop wanted item and delete required items.
                        GameObject craftedItem = Instantiate(outputItem);
                        craftedItem.transform.position = dropLocation.transform.position;
                        Destroy(ITEM1);
                        Destroy(ITEM2);
                    }
                }
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
