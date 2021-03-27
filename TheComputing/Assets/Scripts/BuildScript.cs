﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildScript : MonoBehaviour
{
    public GameObject[] buldings;
    public GameObject testItem; // Test item
    public GameObject[] directionArrow;
    List<GameObject> ch_ShadowBuilding = new List<GameObject>();
    public int num_building_shadow = 0;

    public LayerMask placedBuilding; //Buildings placed by the player

    //send to static gameManger
    public Text t;
    private bool isBuilderOn = true;
    public int buildDirection = 0;
    private GameObject gb;
    //Equpied building
    private int building = 0;
    void Start()
    {
        Gamemanager.moneyText = t;
        for(int i = 0; i < directionArrow.Length; i++)
        {
            ch_ShadowBuilding.Add(Instantiate(directionArrow[i]));
            if (i > 0) ch_ShadowBuilding[i].SetActive(false);
        }

        Gamemanager.moneyText = t;
    }
    void Update()
    {
        if (isBuilderOn)
        {
            PlayerInputs();
            DirectionArrow();
        }
        else if (Input.GetKeyDown("e") && isBuilderOn == false){
            isBuilderOn = true;
            gb.SetActive(false);
        }
    }
    void PlayerInputs()
    {
        // You can build if no object is in the tile
        if (emptyTile() && Input.GetMouseButtonDown(0)) Build();

        if (Input.GetKeyDown("t")) ChangeBuilding();

        if (Input.GetMouseButtonDown(1)) Break();

        if (Input.GetKeyDown("q")) DropItem();
        //opem refiner (Test)
        if (Input.GetKeyDown("e")) RefinerOpen();

        // press r to rotate
        if (Input.GetKeyDown("r") && buildDirection < 3) buildDirection++;
        else if (Input.GetKeyDown("r")) buildDirection = 0;

        // Scroll to rotate building direction
        if (Input.GetAxis("Mouse ScrollWheel") < 0f) // Scroll down
        {
            if (buildDirection < 3) buildDirection++;
            else buildDirection = 0;
        }
        else if (Input.GetAxis("Mouse ScrollWheel") > 0f) // Scroll up
        {
            if (buildDirection > 0) buildDirection--;
            else buildDirection = 3;
        }
    }
    void Build()//test
    {
        GameObject build = Instantiate(buldings[building]);
        build.transform.position = buildPosition();
    }
    void Break()
    {
        Collider2D[] hitColliders = Physics2D.OverlapBoxAll(buildPosition(), new Vector2(0.498f, 0.498f), 0); // Creates a list of colliders in the tile the cursor is over

        foreach (var hitCollider in hitColliders)
        { // Destroys the gameObject
            if(hitCollider.gameObject.tag != "Unb" && hitCollider.gameObject.tag != "Trash Area") //Doesnt destroy gameobj with the unb tag
            {
                Destroy((hitCollider).gameObject);
            }
        }
    }
    void ChangeBuilding()
    {
        if (building == buldings.Length - 1)
        {
            building = 0;
            ch_ShadowBuilding[num_building_shadow].SetActive(false);
            num_building_shadow = 0;
            ch_ShadowBuilding[num_building_shadow].SetActive(true);
        }
        else
        {
            building++;
            num_building_shadow++;
            ch_ShadowBuilding[num_building_shadow].SetActive(true);
            ch_ShadowBuilding[num_building_shadow - 1].SetActive(false);
        }
    }
    void DirectionArrow()
    {
        ch_ShadowBuilding[num_building_shadow].transform.position = buildPosition();
        ch_ShadowBuilding[num_building_shadow].transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, -90 * buildDirection);
    }
    //Looks for gamesObj in a radius of mouse
    GameObject findBuildingGameObject(string tag)
    {
        Collider2D[] c = Physics2D.OverlapCircleAll(buildPosition(), 0.02f);
        GameObject g = null;
        try
        {
            for(int i = 0; i < c.Length; i++)
            {
                if(c[i].tag == tag)
                {
                    g = c[i].gameObject;
                }
            }

        }
        catch
        {
            g = null;
        }
        return g;
    }

    void RefinerOpen()
    {
        GameObject g = findBuildingGameObject("Refiner");
        if(g != null)
        {
            //test so it works (this will be the menu but i havent started with it yet)
            isBuilderOn = false;
            g.gameObject.transform.GetChild(4).gameObject.SetActive(true);
            gb = g.gameObject.transform.GetChild(4).gameObject;
        }
    }
    void DropItem()
    {
        GameObject item = Instantiate(testItem);
        item.transform.position = cursorPosition();
    }
    Vector2 cursorPosition() // The X and Y position from the cursor
    {
        Vector2 temp = Input.mousePosition;
        return Camera.main.ScreenToWorldPoint(temp); // Puts the cursor position into a Vector3 value
    }
    Vector2 buildPosition() // Rounded value of the cursor position to fit into tiles
    {
        return new Vector2(Mathf.Floor(cursorPosition().x) + 0.5f, Mathf.Floor(cursorPosition().y) + 0.5f);
    }
    bool emptyTile()
    {
        if (Physics2D.OverlapCircle(buildPosition(), 0.02f, placedBuilding)) return false;
        else return true;
    }
}
