using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Splitter : MonoBehaviour
{
    public BuildScript buildScript;
    public int direction = 0;
    public int maxCarryingNumber = 5;
    public int speedLVL = 0;

    bool shouldSplit = false;
    const int Up = 0, Down = 2, Right = 1, Left = 3;

    void Start()
    {    
        // Finds the Main Camera and it's BuildScript
        GameObject mainCamera = GameObject.Find("Main Camera");
        buildScript = mainCamera.GetComponent<BuildScript>();

        // Sets the buildings direction to the buildDirection
        direction = buildScript.buildDirection;
        SetRotation();
    }
    void SetRotation()
    {
        transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, -90 * direction);
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Item")
        {
            switch(direction)
            {
                case (Up):
                    if (shouldSplit) col.transform.position = col.transform.position + new Vector3(-1, 0.5f, 0);
                    else col.transform.position = col.transform.position + new Vector3(0, 1.5f, 0);
                    break;
                case (Down):
                    if (shouldSplit) col.transform.position = col.transform.position + new Vector3(1, -0.5f, 0);
                    else col.transform.position = col.transform.position + new Vector3(0, -1.5f, 0);
                    break;
                case (Left):
                    if (shouldSplit) col.transform.position = col.transform.position + new Vector3(-0.5f, -1, 0);
                    else col.transform.position = col.transform.position + new Vector3(-1.5f, 0, 0);
                    break;
                case (Right):
                    if (shouldSplit) col.transform.position = col.transform.position + new Vector3(0.5f, 1, 0);
                    else col.transform.position = col.transform.position + new Vector3(1.5f, 0, 0);
                    break;
            }
            shouldSplit = !shouldSplit;
        }
    }
}