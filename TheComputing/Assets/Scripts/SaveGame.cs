using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveGame
{
    public static void SaveMap()
    {
        BinaryFormatter formater = new BinaryFormatter();
        string path = Application.persistentDataPath + "/mapData.TheComputing";
        FileStream strem = new FileStream(path, FileMode.Create);
        float[] pos = new float[Gamemanager.Buildings.Count * 3];
        int[] id = new int[Gamemanager.Buildings.Count];
        int[] rot = new int[Gamemanager.Buildings.Count];
        //Debug.Log("Yes1 " + Gamemanager.Buildings.Count +" "+ pos.Length);
        //0 1 2: 0 + 3: 3 4 5
        int h = 0;
        for (int i = 0; i < pos.Length - 1; i+=3)
        {
            pos[i] = Gamemanager.Buildings[h].transform.position.x;
            pos[i+1] = Gamemanager.Buildings[h].transform.position.y;
            pos[i+2] = Gamemanager.Buildings[h].transform.position.z;
            h++;
        }
        for (int i = 0; i < id.Length; i++)
        {
            id[i] = Gamemanager.Buildings[i].GetComponent<BuildingId>().id;
            rot[i] = Gamemanager.Buildings[i].GetComponent<BuildingId>().rot;
        }
        MapData data = new MapData(Gamemanager.money, pos, id, rot);

        formater.Serialize(strem, data);
        strem.Close();
    }
    public static MapData loadGame()
    {
        string path = Application.persistentDataPath + "/mapData.TheComputing";
        if (File.Exists(path))
        {
            BinaryFormatter formater = new BinaryFormatter();
            FileStream strem = new FileStream(path, FileMode.Open);

            MapData data = formater.Deserialize(strem) as MapData;
            strem.Close();

            return data;
        }
        else
        {
            Debug.LogError("save file not found in " + path);
            return null;
        }
    }
}
