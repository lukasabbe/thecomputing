using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoCrafter : MonoBehaviour
{
    public GameObject outputItem; // Output item is the item you want to craft, input items are the items required to craft
    public GameObject dropLocation;
    List<int> recipeID = new List<int>();
    void Start()
    {
        Item outputItemScript = outputItem.GetComponent<Item>();

        if (outputItemScript.id == 9) // Recipe for Circuit Board
        { // Recipe
            recipeID.Add(8); // trash plastic
            recipeID.Add(0); // scrap metal
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
                {   // Drop wanted item and delete required items.
                    GameObject craftedItem = Instantiate(outputItem);
                    craftedItem.transform.position = dropLocation.transform.position;
                    Destroy(ITEM1);
                    Destroy(ITEM2);
                }
            }
        }
    }
}
