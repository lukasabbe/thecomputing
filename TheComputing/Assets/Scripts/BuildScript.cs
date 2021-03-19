using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildScript : MonoBehaviour
{
    public GameObject testCube; // For testing the building
    public GameObject testItem; // Test item

    void Update()
    {
        PlayerInputs();
    }
    void PlayerInputs()
    {
        // You can build if no object is in the tile
        if (EmptyTile() && Input.GetMouseButtonDown(0)) Build();

        if (Input.GetMouseButtonDown(1)) Break();

        if (Input.GetKeyDown("q")) DropItem();
    }
    void Build()
    {
        GameObject build = Instantiate(testCube);
        build.transform.position = BuildPosition();
    }
    void Break()
    {
        Collider[] hitColliders = Physics.OverlapSphere(BuildPosition(), 0.1f); // Creates a list of colliders in the tile the cursor is over

        foreach (var hitCollider in hitColliders)
        { // Destroys the gameObject
            Destroy((hitCollider).gameObject);
        }
    }
    void DropItem()
    {
        GameObject item = Instantiate(testItem);
        item.transform.position = CursorPosition();
    }
    Vector3 CursorPosition() // The X and Y position from the cursor
    {
        Vector3 temp = Input.mousePosition;
        float offset = -transform.position.z; // Makes sure the build position is at Z 0
        temp.z = offset; // Set this to be the distance you want the object to be placed in front of the camera.
        return Camera.main.ScreenToWorldPoint(temp); // Puts the cursor position into a Vector3 value
    }
    Vector3 BuildPosition() // Rounded value of the cursor position to fit into tiles
    {
        return new Vector3(Mathf.Floor(CursorPosition().x) + 0.5f, Mathf.Floor(CursorPosition().y) + 0.5f, 0);
    }
    bool EmptyTile()
    {
        if (Physics.CheckSphere(BuildPosition(), 0.1f)) return false;
        else return true;
    }
}
