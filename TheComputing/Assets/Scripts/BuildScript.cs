using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BuildScript : MonoBehaviour
{
    public GameObject[] buldings;
    public GameObject testItem; // Test item
    public GameObject[] directionArrow;
    [HideInInspector]
    public List<GameObject> ch_ShadowBuilding = new List<GameObject>();

    public int num_building_shadow = 0;

    public bool freeBuildings;

    public GameObject BuildMenu;
    private GameObject gb = null;
    public LayerMask placedBuilding; //Buildings placed by the player

    //send to static gameManger
    public Text t;
    public bool isBuilderOn = false;
    public int buildDirection = 0;
    //Equpied building
    [HideInInspector]
    public int building = 0;

    public GameObject escMenu;
    void Start()
    {
        Gamemanager.moneyText = t;
        for(int i = 0; i < directionArrow.Length; i++)
        {
            ch_ShadowBuilding.Add(Instantiate(directionArrow[i]));
            if (i > 0) ch_ShadowBuilding[i].SetActive(false);
        }
        Gamemanager.uppdateText();
    }
    void Update()
    {
        PlayerInputs();
        Gamemanager.AnimateSync+= Time.deltaTime;
        if (Gamemanager.AnimateSync >= 0.267f)
        {
            Gamemanager.AnimateSync = 0;
        }
    }
    void PlayerInputs()
    {
        // You can build if no object is in the tile
        //if (emptyTile() && Input.GetMouseButtonDown(0)) Build();

        if (isBuilderOn)
        {
            if (emptyTile() && Input.GetMouseButtonDown(0)) Build();
            if (Input.GetMouseButtonDown(1)) Break();

            //if (Input.GetKeyDown("t") && !Input.GetKey(KeyCode.LeftControl)) ChangeBuilding(true);
            //else if (Input.GetKeyDown("t") && Input.GetKey(KeyCode.LeftControl)) ChangeBuilding(false);

            // press r to rotate
            if (Input.GetKeyDown("r") && buildDirection < 3) buildDirection++;
            else if (Input.GetKeyDown("r")) buildDirection = 0;
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
            DirectionArrow();
        }

        if (Input.GetKeyDown(KeyCode.Escape)) onEsc();

        if (Input.GetKeyDown(KeyCode.Tab)) isBuild();

        if (Input.GetKeyDown("q")) DropItem();
        //opem refiner (Test)
        if (Input.GetKeyDown("e"))
        {
            RefinerOpen();
            AssemblerOpen();
        }


        // Scroll to rotate building direction

        if (Input.GetKey("h") && Input.GetKey(KeyCode.LeftControl)) removeAllItems();
        if (Input.GetKey("h"))
        {
            GameObject g = findBuildingGameObject(0,"Item");
            Destroy(g);
        }
    }

    void isBuild()
    {
        if (!BuildMenu.activeSelf)
        {
            BuildMenu.SetActive(true);
            isBuilderOn = true;
        }
        else
        {
            BuildMenu.SetActive(false);
            isBuilderOn = false;
            ch_ShadowBuilding[num_building_shadow].SetActive(false);
        }
    }
    void Build()//test
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            if( buldings[building].GetComponent<BuildingId>().cost <= Gamemanager.money || freeBuildings)
            {
                if(!freeBuildings) Gamemanager.money -= buldings[building].GetComponent<BuildingId>().cost;
                Gamemanager.uppdateText();
                GameObject build = Instantiate(buldings[building]);
                Gamemanager.Buildings.Add(build);
                build.transform.position = buildPosition();
                build.GetComponent<BuildingId>().rot = buildDirection;
                build.transform.rotation = Quaternion.Euler(0, 0, -90 * buildDirection);

            }
        }
    }
    void Break()
    {
        Collider2D[] hitColliders = Physics2D.OverlapBoxAll(buildPosition(), new Vector2(0.498f, 0.498f), 0); // Creates a list of colliders in the tile the cursor is over

        foreach (var hitCollider in hitColliders)
        { // Destroys the gameObject
            if(hitCollider.gameObject.tag != "Unb" && hitCollider.gameObject.tag != "Trash Area") //Doesnt destroy gameobj with the unb tag
            {
                if (hitCollider.tag == "Building" || hitCollider.tag == "Refiner") 
                {
                    int index = Gamemanager.Buildings.FindIndex(g => g == hitCollider.gameObject);
                    if(!freeBuildings)Gamemanager.money += hitCollider.gameObject.GetComponent<BuildingId>().cost / 2;
                    Gamemanager.uppdateText();
                    Gamemanager.Buildings.RemoveAt(index);
                }
                Destroy((hitCollider).gameObject);
            }
        }
    }

    void onEsc()
    {
        if (escMenu.activeSelf)
        {
            escMenu.transform.GetChild(1).gameObject.SetActive(false);
            escMenu.SetActive(false);
        }
        else
        {
            escMenu.SetActive(true);
        }
    }

    void ChangeBuilding(bool dir)
    {
        if (dir)
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
        else if(!dir)
        {
            if (building == 0)
            {
                building = buldings.Length -1;
                ch_ShadowBuilding[num_building_shadow].SetActive(false);
                num_building_shadow = buldings.Length - 1;
                ch_ShadowBuilding[num_building_shadow].SetActive(true);
            }
            else
            {
                building--;
                num_building_shadow--;
                ch_ShadowBuilding[num_building_shadow].SetActive(true);
                ch_ShadowBuilding[num_building_shadow + 1].SetActive(false);
            }
        }
    }
    void DirectionArrow()
    {
        ch_ShadowBuilding[num_building_shadow].transform.position = buildPosition();
        ch_ShadowBuilding[num_building_shadow].transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, -90 * buildDirection);
    }
    //Looks for gamesObj in a radius of mouse
    public GameObject findBuildingGameObject(int id, string tag = "null")
    {
        if (tag != "null"){
            Collider2D[] c = Physics2D.OverlapCircleAll(buildPosition(), 0.02f);
            GameObject g = null;
            try
            {
                for (int i = 0; i < c.Length; i++)
                {
                    if (c[i].tag == tag)
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
        }else{
            Collider2D[] c = Physics2D.OverlapCircleAll(buildPosition(), 0.02f);
            GameObject g = null;
            try
            {
                for (int i = 0; i < c.Length; i++)
                {
                    if (c[i].GetComponent<BuildingId>().id == id)
                        g = c[i].gameObject;
                }
            }
            catch
            {
                g = null;
            }
            return g;
        }
    }
    void removeAllItems()
    {

        GameObject[] items;
        items = GameObject.FindGameObjectsWithTag("Item");
        foreach(GameObject i in items)
        {
            Destroy(i);
        }
    }
    void RefinerOpen()
    {
        GameObject g = findBuildingGameObject(0, "Refiner");
        if (g != null)
        {
            if (gb.activeSelf)
            {
                g.gameObject.transform.GetChild(4).gameObject.SetActive(false);
            }
            else
            {
                g.gameObject.transform.GetChild(4).gameObject.SetActive(true);
                gb = g.gameObject.transform.GetChild(4).gameObject;
            }
        }
        else if (gb != null)
        {
            if (gb.activeSelf)
            {
                gb.SetActive(false);
            }
        }
    }
    void AssemblerOpen()
    {
        GameObject assembler = findBuildingGameObject(2);

        if (assembler != null) 
        {
            
            if (!assembler.transform.GetChild(1).gameObject.activeSelf)
            {
                
                assembler.transform.GetChild(1).gameObject.SetActive(true);
                gb= assembler.transform.GetChild(1).gameObject;
            }
        }
        else if (gb != null)
        {
            try
            {
                if (gb.GetComponent<BuildingId>().id == 2) gb.SetActive(false);
            }
            catch
            {

            }
        }
    }
    void DropItem()
    {
        GameObject item = Instantiate(testItem);
        item.transform.position = cursorPosition();
    }
    public Vector2 cursorPosition() // The X and Y position from the cursor
    {
        Vector2 temp = Input.mousePosition;
        return Camera.main.ScreenToWorldPoint(temp); // Puts the cursor position into a Vector3 value
    }
    public Vector2 buildPosition() // Rounded value of the cursor position to fit into tiles
    {
        return new Vector2(Mathf.Floor(cursorPosition().x) + 0.5f, Mathf.Floor(cursorPosition().y) + 0.5f);
    }
    bool emptyTile()
    {
        if (Physics2D.OverlapCircle(buildPosition(), 0.02f, placedBuilding)) return false;
        else return true;
    }

    //saves the game
    void saveGame()
    {
        Debug.Log("Saved game");
        SaveGame.SaveMap(0);
    }
    //loads the files
    public void loadGame(int slot)
    {
        
        MapData g = SaveGame.loadGame(slot);
        if (g == null) return;
        int y = 0;
        for(int i = 0; i < g.buildingID.Length; i++)
        {
            GameObject r = null;
            if (g.buildingID[i] == 0)
            {
                r = Instantiate(buldings[0],new Vector3(g.buildingPos[y], g.buildingPos[y+1], g.buildingPos[y + 2]),Quaternion.identity);
                r.transform.rotation = Quaternion.Euler(0, 0, -90 * g.rotation[i]);
                r.GetComponent<ConveyorBeltManager>().direction = g.rotation[i];
            }
            if (g.buildingID[i] == 1)
            {
                r = Instantiate(buldings[1], new Vector3(g.buildingPos[y], g.buildingPos[y + 1], g.buildingPos[y + 2]), Quaternion.identity);
                r.transform.rotation = Quaternion.Euler(0, 0, -90 * g.rotation[i]);
                r.GetComponent<ConveyorBeltManager>().direction = g.rotation[i];
                r.GetComponent<Refiner>().ch_id = g.Rdata[i].id;
                r.GetComponent<Refiner>().ch_item = g.Rdata[i].item;
            }
            if (g.buildingID[i] == 2)
            {
                r = Instantiate(buldings[5], new Vector3(g.buildingPos[y], g.buildingPos[y + 1], g.buildingPos[y + 2]), Quaternion.identity);
                r.transform.rotation = Quaternion.Euler(0, 0, -90 * g.rotation[i]);
                r.GetComponent<RotateBuilding>().direction = g.rotation[i];
                r.GetComponent<RotateBuilding>().SetRotation();
                
            }
            if (g.buildingID[i] == 3)
            {
                r = Instantiate(buldings[3], new Vector3(g.buildingPos[y], g.buildingPos[y + 1], g.buildingPos[y + 2]), Quaternion.identity);
                r.transform.rotation = Quaternion.Euler(0, 0, -90 * g.rotation[i]);
                r.GetComponent<RotateBuilding>().direction = g.rotation[i];
                r.GetComponent<RotateBuilding>().SetRotation();
            }
            if (g.buildingID[i] == 4)
            {
                r = Instantiate(buldings[2], new Vector3(g.buildingPos[y], g.buildingPos[y + 1], g.buildingPos[y + 2]), Quaternion.identity);
                r.transform.rotation = Quaternion.Euler(0, 0, -90 * g.rotation[i]);
                r.GetComponent<RotateBuilding>().direction = g.rotation[i];
                r.GetComponent<RotateBuilding>().SetRotation();
            }
            if (g.buildingID[i] == 5)
            {
                r = Instantiate(buldings[4], new Vector3(g.buildingPos[y], g.buildingPos[y + 1], g.buildingPos[y + 2]), Quaternion.identity);
                r.transform.rotation = Quaternion.Euler(0, 0, -90 * g.rotation[i]);
            }
            if (g.buildingID[i] == 6)
            {
                r = Instantiate(buldings[6], new Vector3(g.buildingPos[y], g.buildingPos[y + 1], g.buildingPos[y + 2]), Quaternion.identity);
                r.transform.rotation = Quaternion.Euler(0, 0, -90 * g.rotation[i]);
                r.GetComponent<Tunnel>().sh(g.rotation[i]);
            }
            Gamemanager.Buildings.Add(r);
            r.GetComponent<BuildingId>().id = g.buildingID[i];
            r.GetComponent<BuildingId>().rot = g.rotation[i];
            y += 3;
        }



        Gamemanager.money = g.currentMoney;
        Gamemanager.uppdateText();
    }
}
