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
    public bool[] opt;
    public refinerData[] Rdata;


    public MapData(int money, float[] buildingPos, int[] buildingID, int[] rotation, bool[] opt, refinerData[] Rdata)
    {
        this.currentMoney = money;
        this.buildingPos = buildingPos;
        this.buildingID = buildingID;
        this.rotation = rotation;
        this.opt = opt;
        this.Rdata = Rdata;

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
