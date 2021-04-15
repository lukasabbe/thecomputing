using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public int id = 0; //all items can only have 1 id
    public string ItemName;
    public string ItemDescription;
    public int SellPrice = 0;
    public List<int> refinsTo = new List<int>(); //refins list. If the item can be refind you can put in the ids of all the items it refines in to

    public GameObject autoC;
    RecipieManager[] recipieObj;
    private void Awake(){
        recipieObj = autoC.GetComponent<AutoCrafter>().recipieObj;
        if (id >= 13 && id != 43)
            CalculateItemSellprice();
    }

    void CalculateItemSellprice(){
        Debug.LogError("CALCULATING SELLPRICE");
        int totalSellPrice = 0;
        int uniqueItems = 0;
        for (int i = 0; i < recipieObj.Length; i++){
            if(id == recipieObj[i].item.GetComponent<Item>().id){
                uniqueItems = recipieObj[i].itemIds.Length;
                for (int i1 = 0; i1 < recipieObj[i].itemIds.Length; i1++){
                    totalSellPrice = autoC.GetComponent<AutoCrafterUI>().GetItem(recipieObj[i].itemIds[i1]).GetComponent<Item>().SellPrice;
                }
            }
        }
        totalSellPrice *= uniqueItems;
        SellPrice = totalSellPrice;
        Debug.Log("SELLPRICE: " + SellPrice);
    }
}
