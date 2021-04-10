using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class PauseMenu : MonoBehaviour
{

    public GameObject menu;
    private GameObject[] bt = new GameObject[3];
    public GameObject saveMenu;
    public List<Button> saveBt = new List<Button>();
    public GameObject cammra;
    private bool isLoad;
    // Start is called before the first frame update
    void Awake()
    {
        //gets the bts
        bt[0] = menu.transform.GetChild(1).gameObject;
        bt[1] = menu.transform.GetChild(2).gameObject;
        bt[2] = menu.transform.GetChild(3).gameObject;
        //makes the btns
        bt[0].GetComponent<Button>().onClick.AddListener(onSave);
        bt[1].GetComponent<Button>().onClick.AddListener(onLoad);
        bt[2].GetComponent<Button>().onClick.AddListener(onQuit);
        //lisents for save button click
        saveBt[0].onClick.AddListener(delegate { onSlot(0); });
        saveBt[1].onClick.AddListener(delegate { onSlot(1); });
        saveBt[2].onClick.AddListener(delegate { onSlot(2); });
        saveBt[3].onClick.AddListener(delegate { onSlot(3); });
        saveBt[4].onClick.AddListener(delegate { onSlot(4); });

    }
    void onSave()
    {
        if (saveMenu.activeSelf == true)
        {
            saveMenu.SetActive(false);
            isLoad = false;
        }
        else if (saveMenu.activeSelf == false)
        {
            saveMenu.SetActive(true);
            saveMenu.transform.GetChild(0).GetComponent<Text>().text = "save slot";
            isLoad = false;
        }
    }
    void onLoad()
    {
        Debug.Log(saveMenu.activeSelf);
        if(saveMenu.activeSelf == true)
        {
            saveMenu.SetActive(false);
            isLoad = false;
        }
        else if (saveMenu.activeSelf == false)
        {
            saveMenu.SetActive(true);
            saveMenu.transform.GetChild(0).GetComponent<Text>().text = "Load slot";
            isLoad = true;
        }
    }
    void onQuit()
    {
        SceneManager.LoadScene(0);
    }
    void onSlot(int slotnum)
    {
        Debug.Log("slot: " + slotnum);
        string path = Application.persistentDataPath + "/mapData" + slotnum + ".TheComputing";
        if (isLoad == true && File.Exists(path))
        {
            int co = Gamemanager.Buildings.Count;
            for (int i = 0; i < co; i++)
            {
                Destroy(Gamemanager.Buildings[i]);

            }
            Debug.Log(Gamemanager.Buildings.Count);
            cammra.GetComponent<BuildScript>().loadGame(slotnum);
        }
        else if (isLoad == false)
        {
            SaveGame.SaveMap(slotnum);
        }
    }
}
