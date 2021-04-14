using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Recipe", menuName = "Recipe")]

public class RecipieManager : ScriptableObject
{
    [Tooltip(@"  ----------Item ids----------

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

    ----------Item ids---------- ")]
    public int[] itemIds;
    public int[] itemAmounts;

    public int itemsCraftedPerTime = 1;

    public GameObject item; 
}
