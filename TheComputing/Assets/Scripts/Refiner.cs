using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Refiner : MonoBehaviour
{
    //The chosen id in UI of game
    public GameObject ch_item_prefab;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Item")
        {
            //Gets the component
            Item i = ch_item_prefab.GetComponent<Item>();
            collision.GetComponent<Item>().id = i.id;
            collision.GetComponent<Item>().ItemName = i.ItemName;
            collision.GetComponent<Item>().SellPrice = i.SellPrice;
            collision.gameObject.GetComponent<SpriteRenderer>().sprite = ch_item_prefab.GetComponent<SpriteRenderer>().sprite;

        }
    }
}
