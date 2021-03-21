using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildScript : MonoBehaviour
{
    public GameObject[] buldings;
    public GameObject testItem; // Test item

    public int buildDirection = 0;

    //Equpied building
    private int building = 0;
    void Update()
    {
        PlayerInputs();
    }
    void PlayerInputs()
    {
        // You can build if no object is in the tile
        if (EmptyTile() && Input.GetMouseButtonDown(0)) Build();

        if (Input.GetKeyDown("t")) ChangeBuilding();

        if (Input.GetMouseButtonDown(1)) Break();

        if (Input.GetKeyDown("q")) DropItem();
        //opem refiner
        if (Input.GetKeyDown("e")) RefinerOpen();

        if (Input.GetKeyDown("r") && buildDirection < 3) buildDirection++;
        else if (Input.GetKeyDown("r")) buildDirection = 0;
    }
    void Build()
    {
        GameObject build = Instantiate(buldings[building]);
        build.transform.position = BuildPosition();
    }
    void Break()
    {
        Collider2D[] hitColliders = Physics2D.OverlapBoxAll(BuildPosition(), new Vector2(0.498f, 0.498f), 0); // Creates a list of colliders in the tile the cursor is over

        foreach (var hitCollider in hitColliders)
        { // Destroys the gameObject
            Destroy((hitCollider).gameObject);
        }
    }
    void ChangeBuilding()
    {
        if(building == 0) building = 1;
        else building = 0;
    }
    //Looks for gamesObj in a radius of mouse
    GameObject findBuildingGameObject()
    {
        Collider2D c = Physics2D.OverlapCircle(BuildPosition(), 0.02f);
        GameObject g;
        try
        {
            g = c.gameObject;
        }
        catch
        {
            g = null;
        }
        return g;
    }

    void RefinerOpen()
    {
        GameObject g = findBuildingGameObject();
        if(g != null)
        {
            if (g.tag == "Refiner")
            {
                //test so it works (this will be the menu but i havent started with it yet)
                g.SetActive(false);
            }
        }
    }

    void DropItem()
    {
        GameObject item = Instantiate(testItem);
        item.transform.position = CursorPosition();
    }
    Vector2 CursorPosition() // The X and Y position from the cursor
    {
        Vector2 temp = Input.mousePosition;
        return Camera.main.ScreenToWorldPoint(temp); // Puts the cursor position into a Vector3 value
    }
    Vector2 BuildPosition() // Rounded value of the cursor position to fit into tiles
    {
        return new Vector2(Mathf.Floor(CursorPosition().x) + 0.5f, Mathf.Floor(CursorPosition().y) + 0.5f);
    }
    bool EmptyTile()
    {
        if (Physics2D.OverlapCircle(BuildPosition(), 0.02f)) return false;
        else return true;
    }
}
