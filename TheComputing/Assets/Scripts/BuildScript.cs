using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildScript : MonoBehaviour
{
    public GameObject[] buldings;
    public GameObject testItem; // Test item
    public GameObject directionArrow;

    //send to static gameManger
    public Text t;

    public int buildDirection = 0;

    //Equpied building
    private int building = 0;
    void Start()
    {
        directionArrow = Instantiate(directionArrow);
        GameManger.t = t;
    }
    void Update()
    {
        PlayerInputs();
        DirectionArrow();
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

        if (Input.GetKeyDown("r") && buildDirection < 3) buildDirection++;
        else if (Input.GetKeyDown("r")) buildDirection = 0;
    }
    void Build()
    {
        GameObject build = Instantiate(buldings[building]);
        build.transform.position = buildPosition();
    }
    void Break()
    {
        Collider2D[] hitColliders = Physics2D.OverlapBoxAll(buildPosition(), new Vector2(0.498f, 0.498f), 0); // Creates a list of colliders in the tile the cursor is over

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
    void DirectionArrow()
    {
        directionArrow.transform.position = buildPosition();
        directionArrow.transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, -90 * buildDirection);
    }
    //Looks for gamesObj in a radius of mouse
    GameObject findBuildingGameObject()
    {
        Collider2D c = Physics2D.OverlapCircle(buildPosition(), 0.02f);
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
        if (Physics2D.OverlapCircle(buildPosition(), 0.02f)) return false;
        else return true;
    }
}
