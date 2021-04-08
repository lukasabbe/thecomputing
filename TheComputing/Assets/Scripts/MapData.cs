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

    public MapData(int money, float[] buildingPos, int[] buildingID, int[] rotation)
    {
        this.currentMoney = money;
        this.buildingPos = buildingPos;
        this.buildingID = buildingID;
        this.rotation = rotation;
    }
}
