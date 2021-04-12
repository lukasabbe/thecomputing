using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Recipe", menuName = "Recipe")]
public class RecipieManager : ScriptableObject
{
    public int[] itemIds;
    public int[] itemAmounts;

    public GameObject item; 
}
