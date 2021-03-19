using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Refiner : MonoBehaviour
{
    //The chosen id in UI of game
    public GameObject ch_item_prefab;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Detectet");
        if(collision.gameObject.tag == "Item")
        {
            //Gets the component
            Item item = collision.GetComponent<Item>();
            item = ch_item_prefab.GetComponent<Item>();
            gameObject.GetComponent<SpriteRenderer>().sprite = ch_item_prefab.GetComponent<SpriteRenderer>().sprite;

        }
    }
}
