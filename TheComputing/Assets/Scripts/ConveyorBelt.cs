using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBelt : MonoBehaviour
{
    public BuildScript buildScript;
    public int direction = 0;
    public int maxCarryingNumber = 5;
    public int speedLVL = 0;

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
    void Update()
    {
        
    }
    void SetRotation()
    {
        transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, -90 * direction);
    }
    public void OnTriggerStay2D(Collider2D col)
    {   
        if (col.gameObject.tag == "Item") 
        { 
            // Forward pushing force
            if (direction == Up)
                col.transform.position = new Vector2(col.transform.position.x, col.transform.position.y + 1 * Time.deltaTime + 0.5f * speedLVL * Time.deltaTime);
            if (direction == Down) 
                col.transform.position = new Vector2(col.transform.position.x, col.transform.position.y - 1 * Time.deltaTime - 0.5f * speedLVL * Time.deltaTime);
            if (direction == Right) 
                col.transform.position = new Vector2(col.transform.position.x + 1 * Time.deltaTime + 0.5f * speedLVL * Time.deltaTime, col.transform.position.y);
            if (direction == Left) 
                col.transform.position = new Vector2(col.transform.position.x - 1 * Time.deltaTime - 0.5f * speedLVL * Time.deltaTime, col.transform.position.y);

            // posDifference is the distance from the belt to the item
            Vector2 posDifference = col.transform.position - transform.position; 

            // Lowers that number so item goes closer to the center of the conveyor belt
            posDifference = new Vector2(col.transform.position.x - posDifference.x / (2000 * Time.deltaTime), 
                                        col.transform.position.y - posDifference.y / (2000 * Time.deltaTime));


            // Push the items closer to the middle of the conveyor belt
            if (direction == Up)
                col.transform.position = new Vector2(posDifference.x, col.transform.position.y);
            if (direction == Down)
                col.transform.position = new Vector2(posDifference.x, col.transform.position.y);
            if (direction == Right)
                col.transform.position = new Vector2(col.transform.position.x, posDifference.y);
            if (direction == Left)
                col.transform.position = new Vector2(col.transform.position.x, posDifference.y);

        }
    }
}