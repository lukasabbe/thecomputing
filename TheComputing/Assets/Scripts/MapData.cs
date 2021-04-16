using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MapData
{

    public int currentMoney;
    public float[] buildingPos;
    public int[] buildingID;
    public int[] rotation;
    public bool[] refinerOpt;
    public refinerData[] Rdata;
    public bool[] crafterOpt;
    public int[] crafterRecepiId;
    public sorter[] spliterData;


    public MapData(int money, float[] buildingPos, int[] buildingID, int[] rotation, bool[] Ropt, refinerData[] Rdata, bool[] cOpt, int[] CrafterRepID, sorter[] SpliterData)
    {
        this.currentMoney = money;
        this.buildingPos = buildingPos;
        this.buildingID = buildingID;
        this.rotation = rotation;
        this.refinerOpt = Ropt;
        this.Rdata = Rdata;
        this.crafterOpt = cOpt;
        this.crafterRecepiId = CrafterRepID;
        this.spliterData = SpliterData;

    }
}
[System.Serializable]
public class refinerData
{
    public int id;
    public int item;

    public refinerData(int id, int item)
    {
        this.id = id;
        this.item = item;
    }
}

[System.Serializable]
public class sorter
{
    public int dir;
    public int splitDir;
    public sorter(int dir, int splitDir)
    {
        this.dir = dir;
        this.splitDir = splitDir;
    }
}

