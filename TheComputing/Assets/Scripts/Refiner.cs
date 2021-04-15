using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
public class Refiner : MonoBehaviour
{
    /*
     ----------Item ids----------

    ScrapMetal      : 0
    TrashPlastic    : 1

    Plastic         : 2
    Polymer         : 3
    TrashCopper     : 4
    TrashGold       : 5
    TrashIron       : 6
    TrashSilver     : 7

    Copper          : 8
    Gold            : 9
    Iron            : 10
    Rubber          : 11
    Silver          : 12

    Low quality Cables           : 13
    Low quality Case             : 14
    Low quality CircuitBoard     : 15
    Low quality CPU              : 16
    Low quality GPU              : 17
    Low quality Harddrive        : 18
    Low quality Motherboard      : 19
    Low quality PSU              : 20
    Low quality RAM              : 21
    Low quality SSD              : 22

    Cables                       : 23
    Case                         : 24
    CircuitBoard                 : 25
    CPU                          : 26
    GPU                          : 27
    Harddrive                    : 28
    Motherboard                  : 29
    PSU                          : 30
    RAM                          : 31
    SSD                          : 32

    High quality Cables          : 33
    High quality Case            : 34
    High quality CircuitBoard    : 35
    High quality CPU             : 36
    High quality GPU             : 37
    High quality Harddrive       : 38
    High quality Motherboard     : 39
    High quality PSU             : 40
    High quality RAM             : 41
    High quality SSD             : 42

    ----------Item ids---------- 

    */

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
        if(pos == 0)//trach gold
        {
            return 5;
        }
        if (pos == 1)//trach silver
        {
            return 7;
        }
        if (pos == 2)//plastic
        {
            return 2;
        }
        if (pos == 3)//rubber
        {
            return 11;
        }
        if (pos == 4)//trash copper
        {
            return 4;
        }
        if (pos == 5)//trash iron
        {
            return 6;
        }
        if (pos == 6)//Gold
        {
            return 9;
        }
        if (pos == 7)//Silver
        {
            return 12;
        }
        if (pos == 8)//Coppar
        {
            return 8;
        }
        if (pos == 9)//Iron
        {
            return 10;
        }
        if (pos == 10)//Polemyr
        {
            return 3;
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
        collision.GetComponent<Item>().refinsTo = i.refinsTo;
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
