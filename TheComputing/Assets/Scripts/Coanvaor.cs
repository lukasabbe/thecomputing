﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coanvaor : MonoBehaviour
{
    public bool hasSynced = false;
    
    private void Start()
    {
        gameObject.transform.rotation = Quaternion.Euler(0, 0,0);
        setChild(false);
        if (gameObject.GetComponent<BuildingId>().rot == 0)
        {
            gameObject.GetComponent<BuildingId>().indexActiveted = 0;
        }
        else if (gameObject.GetComponent<BuildingId>().rot == 1)
        {
            gameObject.GetComponent<BuildingId>().indexActiveted = 12;
        }
        else if (gameObject.GetComponent<BuildingId>().rot == 2)
        {
            gameObject.GetComponent<BuildingId>().indexActiveted = 4;
        }
        else if (gameObject.GetComponent<BuildingId>().rot == 3)
        {
            gameObject.GetComponent<BuildingId>().indexActiveted = 8;
        }
        List<GameObject> g = new List<GameObject>();
        g.Add(serch(0, 1));
        g.Add(serch(0, -1));
        g.Add(serch(1, 0));
        g.Add(serch(-1, 0));
        /*
         * t: Obejctet som vi letar efter
         * rot: Vilket rotation obj behöver
         * i: Vilken av åvan som vi titta påsd
         */
        for(int i = 0; i < g.Count; i++)
        {
            if(g[i] != null)
            {
                int t;
                t = g[i].GetComponent<BuildingId>().indexActiveted;
                Debug.Log("t:" + t + " i: " + i + " rot:" + gameObject.GetComponent<BuildingId>().rot);
                //Conver svängar
                if (t == 0 && i == 1 && gameObject.GetComponent<BuildingId>().rot == 1)//up sväng höger
                {
                    setChild(false);
                    gameObject.GetComponent<BuildingId>().indexActiveted = 19;
                }
                if (t == 0 && i == 1 && gameObject.GetComponent<BuildingId>().rot == 3)//up sväng vänster
                {
                    setChild(false);
                    gameObject.GetComponent<BuildingId>().indexActiveted = 21;
                }
                if (t == 4 && i == 0 && gameObject.GetComponent<BuildingId>().rot == 3)//ner sväng vänster
                {
                    setChild(false);
                    gameObject.GetComponent<BuildingId>().indexActiveted = 20;
                }
                if (t == 4 && i == 0 && gameObject.GetComponent<BuildingId>().rot == 1)//ner sväng höger
                {
                    setChild(false);
                    gameObject.GetComponent<BuildingId>().indexActiveted = 18;
                }
                if (t == 12 && i == 3 && gameObject.GetComponent<BuildingId>().rot == 0)//höger sväng up
                {
                    setChild(false);
                    gameObject.GetComponent<BuildingId>().indexActiveted = 17;
                }
                if (t == 12 && i == 3 && gameObject.GetComponent<BuildingId>().rot == 2)//höger sväng ner
                {
                    setChild(false);
                    gameObject.GetComponent<BuildingId>().indexActiveted = 16;
                }
                if (t == 8 && i == 2 && gameObject.GetComponent<BuildingId>().rot == 0)//vänster sväng up
                {
                    setChild(false);
                    gameObject.GetComponent<BuildingId>().indexActiveted = 23;
                }
                if (t == 8 && i == 2 && gameObject.GetComponent<BuildingId>().rot == 2)//vänster sväng ner
                {
                    setChild(false);
                    gameObject.GetComponent<BuildingId>().indexActiveted = 22;
                }
                //Sväng svängar
                if (t == 19 && i == 3 && gameObject.GetComponent<BuildingId>().rot == 0)//från höger sväng up
                {
                    setChild(false);
                    gameObject.GetComponent<BuildingId>().indexActiveted = 17;
                }
                if (t == 19 && i == 3 && gameObject.GetComponent<BuildingId>().rot == 2)//från höger sväng ner
                {
                    setChild(false);
                    gameObject.GetComponent<BuildingId>().indexActiveted = 16;
                }
                if (t == 21 && i == 2 && gameObject.GetComponent<BuildingId>().rot == 0)//från vänster sväng up
                {
                    setChild(false);
                    gameObject.GetComponent<BuildingId>().indexActiveted = 23;
                }
                if (t == 21 && i == 2 && gameObject.GetComponent<BuildingId>().rot == 2)//från vänster sväng ner
                {
                    setChild(false);
                    gameObject.GetComponent<BuildingId>().indexActiveted = 22;
                }

            }
        }
        setChild(true);
        hasSynced = false;
    }
    public void Update()
    {
        if(Gamemanager.AnimateSync == 0f && !hasSynced)
        {
            Debug.Log("enterd");
            SyncCon();
            hasSynced = true;
        }
    }
    public void SyncCon()
    {
        gameObject.transform.GetChild(gameObject.GetComponent<BuildingId>().indexActiveted).GetComponent<Animator>().SetBool("Start",false);
        Debug.Log(gameObject.transform.GetChild(gameObject.GetComponent<BuildingId>().indexActiveted).GetComponent<Animator>().GetBool("Start"));
    }
    public void setChild(bool g)
    {
        gameObject.transform.GetChild(gameObject.GetComponent<BuildingId>().indexActiveted).gameObject.SetActive(g);
    }
    public GameObject serch(int x, int y)
    {
        GameObject g = null;
        Collider2D[] gb;
        gb = Physics2D.OverlapCircleAll(new Vector2(gameObject.transform.position.x + x, gameObject.transform.position.y + y), 0.2f);
        foreach(Collider2D h in gb)
        {
            if (h.gameObject.tag == "Building")
            {
                g = h.gameObject;
            }
        }
        return g;
    }
}
