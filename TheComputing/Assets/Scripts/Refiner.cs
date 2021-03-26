using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
public class Refiner : MonoBehaviour
{
    //list of matiral
    //*0 = gold
    //*1 = silver
    //ID list
    //*1 = gold
    //*2 = silver

    public List<Button> RefinerButtonItem = new List<Button>();
    //The chosen id in UI of game
    public List<GameObject> ch_item_prefab;
    public int ch_item;
    private void Start()
    {
        //scrap buttons
        RefinerButtonItem[0].onClick.AddListener(changeItemGold);
        RefinerButtonItem[1].onClick.AddListener(changeItemSilver);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Item")
        {
            //Gets the component
            Item i = ch_item_prefab[ch_item].GetComponent<Item>();
            Item it = collision.GetComponent<Item>();
            for (int b = 0; b < it.refinsTo.Count; b++)
            {
                //Debug.Log("Count: " + i.refinsTo.Count + "\nItem now: " + i.refinsTo[b] + "\nch_item: " + ch_item);
                if(it.refinsTo[b] == ch_item + 1) 
                {
                    collision.GetComponent<Item>().id = i.id;
                    collision.GetComponent<Item>().ItemName = i.ItemName;
                    collision.GetComponent<Item>().SellPrice = i.SellPrice;
                    collision.gameObject.GetComponent<SpriteRenderer>().sprite = ch_item_prefab[ch_item].GetComponent<SpriteRenderer>().sprite;
                }
            }

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
