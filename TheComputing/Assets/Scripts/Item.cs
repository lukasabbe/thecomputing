using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public int id = 0;
    public string ItemName;
    public int SellPrice = 0;
    public List<int> refinsTo = new List<int>();
}
