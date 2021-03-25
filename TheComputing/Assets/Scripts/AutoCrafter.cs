using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoCrafter : MonoBehaviour
{
    public GameObject outputItem; // Output item is the item you want to craft, input items are the items required to craft
    int[] inputID;
    public GameObject dropLocation;
    void Start()
    {
        Item outputItemScript = outputItem.GetComponent<Item>();

        if (outputItemScript.id == 9) // Recipe for Circuit Board
        { // Recipe
            inputID[0] = 8; // trash plastic
            inputID[1] = 0; // scrap metal
        }
    }
    void Update()
    {
        Crafter();
    }
    void Crafter()
    {
        Collider2D[] hitColliders = Physics2D.OverlapBoxAll(transform.position, new Vector2(0.498f, 0.498f), 0); // Creates a list of colliders in the tile the cursor is over
        
        GameObject[] ITEM = null;
        foreach (Collider2D hitCollider in hitColliders)
        {
            if (hitCollider.gameObject.tag == "Item") // Checks if the required items are in the collider
            {
                if (hitCollider.gameObject.GetComponent<Item>().id == inputID[0]) ITEM[0] = (hitCollider).gameObject;
                if (hitCollider.gameObject.GetComponent<Item>().id == inputID[1]) ITEM[1] = (hitCollider).gameObject;

                // If all required materials are found, craft item
                if (ITEM[0] != null && ITEM[1] != null)
                {   // Drop wanted item and delete required items
                    GameObject craftedItem = Instantiate(outputItem);
                    craftedItem.transform.position = dropLocation.transform.position;

                    Destroy(ITEM[0]);
                    Destroy(ITEM[1]);
                }
            }
        }
    }
    /*
    public void OnTriggerStay2D(Collider2D[] col)
    {
        GameObject[] ITEM = null;
        foreach (Collider2D hitCollider in col)
        {
            if (hitCollider.gameObject.tag == "Item") // Checks if the required items are in the collider
            {
                if (hitCollider.GetComponent<Item>().id == inputID[0]) ITEM[0] = (hitCollider).gameObject;
                if (hitCollider.GetComponent<Item>().id == inputID[1]) ITEM[1] = (hitCollider).gameObject;
            }

            // If all required materials are found, craft item
            if (ITEM[0] != null && ITEM[1] != null)
            {   // Drop wanted item and delete required items
                GameObject craftedItem = Instantiate(outputItem);
                craftedItem.transform.position = dropLocation.transform.position;

                Destroy(ITEM[0]);
                Destroy(ITEM[1]);
            }
        }
    }
    */
}
