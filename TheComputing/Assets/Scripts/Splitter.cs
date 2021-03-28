﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Splitter : MonoBehaviour
{
    public LayerMask itemLayer, conveyorLayer;

    public BuildScript buildScript;
    public int direction = 0;

    bool shouldSplit = false;
    const int Up = 0, Down = 2, Right = 1, Left = 3;

    void Start(){ 
        // Finds the Main Camera and it's BuildScript
        GameObject mainCamera = GameObject.Find("Main Camera");
        buildScript = mainCamera.GetComponent<BuildScript>();

        // Sets the buildings direction to the buildDirection
        direction = buildScript.buildDirection;
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        Vector3 itemCheckOffset = Vector3.zero;
        Vector3 conveyorCheckOffset = Vector3.zero;

        switch (direction){
            case (Up):
                itemCheckOffset = new Vector3(0, -0.6f, 0); break;
            case (Down):
                itemCheckOffset = new Vector3(0, 0.6f, 0); break;
            case (Left):
                itemCheckOffset = new Vector3(0.6f, 0, 0); break;
            case (Right):
                itemCheckOffset = new Vector3(-0.6f, 0, 0); break;
        }

        if (col.tag == "Item" && Physics2D.OverlapBox(transform.position + itemCheckOffset, new Vector2(0.5f, 0.5f), 0, conveyorLayer))
        {
            switch(direction){
                case (Up):
                    itemCheckOffset = new Vector3(0, -0.6f, 0); break;
                case (Down):
                    itemCheckOffset = new Vector3(0, 0.6f, 0); break;
                case (Left):
                    itemCheckOffset = new Vector3(0.6f, 0, 0); break;
                case (Right):
                    itemCheckOffset = new Vector3(-0.6f, 0, 0); break;
            }
            if (Physics2D.OverlapBox(transform.position + itemCheckOffset, new Vector2(0.4f, 0.4f), 0, itemLayer)){
                GameObject itemToSwitch = Physics2D.OverlapBox(transform.position + itemCheckOffset, new Vector2(0.4f, 0.4f), 0, itemLayer).gameObject;
                if (shouldSplit && direction == Up) itemToSwitch.transform.position = transform.position + new Vector3(1, 0, 0);
                if (shouldSplit && direction == Down) itemToSwitch.transform.position = transform.position + new Vector3(-1, 0, 0);
                if (shouldSplit && direction == Left) itemToSwitch.transform.position = transform.position + new Vector3(0, -1, 0);
                if (shouldSplit && direction == Right) itemToSwitch.transform.position = transform.position + new Vector3(0, 1, 0);
            }
            shouldSplit = !shouldSplit;
        }
    }
}