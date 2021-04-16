using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveGame
{
    public static void SaveMap(int slot)
    {
        if(slot >= 0)
        {
            //creates a formater 
            BinaryFormatter formater = new BinaryFormatter();
            string path = Application.persistentDataPath + "/mapData"+slot+".TheComputing";
            FileStream strem = new FileStream(path, FileMode.Create);
            //creats all arrays for saving. 
            float[] pos = new float[Gamemanager.Buildings.Count * 3];
            int[] id = new int[Gamemanager.Buildings.Count];
            int[] rot = new int[Gamemanager.Buildings.Count];
            bool[] opt = new bool[Gamemanager.Buildings.Count];
            bool[] opt2 = new bool[Gamemanager.Buildings.Count];
            //Class that saves refiners data
            refinerData[] Rdata = new refinerData[Gamemanager.Buildings.Count];
            int[] Cdata = new int[Gamemanager.Buildings.Count];
            sorter[] spliterData = new sorter[Gamemanager.Buildings.Count];
            //Debug.Log("Yes1 " + Gamemanager.Buildings.Count +" "+ pos.Length);
            //0 1 2: 0 + 3: 3 4 5
            int h = 0;
            for (int i = 0; i < pos.Length - 1; i += 3)
            {
                pos[i] = Gamemanager.Buildings[h].transform.position.x;
                pos[i + 1] = Gamemanager.Buildings[h].transform.position.y;
                pos[i + 2] = Gamemanager.Buildings[h].transform.position.z;
                h++;
            }
            for (int i = 0; i < id.Length; i++)
            {
                id[i] = Gamemanager.Buildings[i].GetComponent<BuildingId>().id;
                if (id[i] == 1)
                {
                    opt[i] = true;
                    Rdata[i] = new refinerData(Gamemanager.Buildings[i].GetComponent<Refiner>().ch_id, Gamemanager.Buildings[i].GetComponent<Refiner>().ch_item);
                }
                else if(id[i] == 2)
                {
                    opt2[i] = true;
                    Cdata[i] = Gamemanager.Buildings[i].GetComponent<AutoCrafter>().selectedRecipeIndex;
                }
                else if(id[i] == 5)
                {
                    spliterData[i] = new sorter(Gamemanager.Buildings[i].GetComponent<Splitter>().direction, Gamemanager.Buildings[i].GetComponent<Splitter>().splitDirection);
                }
                else
                {
                    opt[i] = false;
                    opt2[i] = false;
                    Rdata[i] = null;
                    Cdata[i] = -100;
                    spliterData[i] = null;
                }
                rot[i] = Gamemanager.Buildings[i].GetComponent<BuildingId>().rot;
            }
            MapData data = new MapData(Gamemanager.money, pos, id, rot, opt, Rdata,opt2,Cdata,spliterData);

            formater.Serialize(strem, data);
            strem.Close();
        }
    }
    public static MapData loadGame(int slot)
    {
        string path = Application.persistentDataPath + "/mapData" + slot + ".TheComputing";
        Debug.Log(path);
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
            Debug.Log("save file not found in " + path);
            return null;
        }
    }
}
