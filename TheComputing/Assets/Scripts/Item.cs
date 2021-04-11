using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public int id = 0; //all items can only have 1 id
    public string ItemName; 
    public int SellPrice = 0;
    public List<int> refinsTo = new List<int>(); //refins list. If the item can be refind you can put in the ids of all the items it refines in to
}
