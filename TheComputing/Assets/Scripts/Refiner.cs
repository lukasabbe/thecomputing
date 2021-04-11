using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
public class Refiner : MonoBehaviour
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

    public List<Button> RefinerButtonItem = new List<Button>();
    //The chosen id in UI of game
    public List<GameObject> ch_item_prefab;
    public int ch_item;
    public int ch_id;
    public int refinerWait;
    private void Start()
    {
        //scrap buttons
        //this for loop loads all the buttons with the right item.
        for(int i = 0; i <RefinerButtonItem.Count; i++)
        {
            obj item = new obj(findId(i),i);
            //Debug.Log(item.id + "\n"+item.pos);
            RefinerButtonItem[i].onClick.AddListener(delegate { changeItem(item); });
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Item")
        {
            //Gets the component
            Item i = ch_item_prefab[ch_item].GetComponent<Item>();
            Item it = collision.GetComponent<Item>();
            for (int b = 0; b < it.refinsTo.Count; b++)
            {
                //Debug.Log("Count: " + i.refinsTo.Count + "\nItem now: " + i.refinsTo[b] + "\nch_item: " + ch_item);
                //checks so the items is right. 
                if (it.refinsTo[b] == ch_id)
                {
                    StartCoroutine(timer(refinerWait, collision, i));
                }
            }

        }
    }
    void changeItem(obj item)
    {
        ch_item = item.pos;
        ch_id = item.id;
    }
    int findId(int pos)
    {
        if(pos == 0)//gold
        {
            return 1;
        }
        if (pos == 1)//silver
        {
            return 2;
        }
        if (pos == 2)//plastic
        {
            return 5;
        }
        if (pos == 3)//rubber
        {
            return 7;
        }
        return 100;
    }
    IEnumerator timer(int waitTime, Collider2D collision, Item i)
    {
        gameObject.GetComponent<ConveyorBeltManager>().isOn = false;
        yield return new WaitForSeconds(waitTime);
        gameObject.GetComponent<ConveyorBeltManager>().isOn = true;
        collision.GetComponent<Item>().id = i.id;
        collision.GetComponent<Item>().ItemName = i.ItemName;
        collision.GetComponent<Item>().SellPrice = i.SellPrice;
        collision.gameObject.GetComponent<SpriteRenderer>().sprite = ch_item_prefab[ch_item].GetComponent<SpriteRenderer>().sprite;
        collision.gameObject.GetComponent<SpriteRenderer>().color = ch_item_prefab[ch_item].GetComponent<SpriteRenderer>().color;
    }
}
class obj
{
    public int pos;
    public int id;
    public obj(int id, int pos)
    {
        this.pos = pos;
        this.id = id;
    }
}
