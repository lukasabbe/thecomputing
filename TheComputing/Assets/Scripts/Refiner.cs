using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
public class Refiner : MonoBehaviour
{
    //list of matiral
    //*0 = gold
    //*1 = silver


    public List<Button> RefinerButtonItem = new List<Button>();
    //The chosen id in UI of game
    public List<GameObject> ch_item_prefab;
    public int ch_item;
    private void Start()
    {
        RefinerButtonItem[0].onClick.AddListener(changeItemGold);
        RefinerButtonItem[1].onClick.AddListener(changeItemSilver);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Item")
        {
            //Gets the component
            Item i = ch_item_prefab[ch_item].GetComponent<Item>();
            collision.GetComponent<Item>().id = i.id;
            collision.GetComponent<Item>().ItemName = i.ItemName;
            collision.GetComponent<Item>().SellPrice = i.SellPrice;
            collision.gameObject.GetComponent<SpriteRenderer>().sprite = ch_item_prefab[ch_item].GetComponent<SpriteRenderer>().sprite;

        }
    }
    void changeItemSilver()
    {
        ch_item = 1;
    }
    void changeItemGold()
    {
        ch_item = 0;
    }
}
