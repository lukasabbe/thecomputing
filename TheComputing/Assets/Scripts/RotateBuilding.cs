using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateBuilding : MonoBehaviour
{
    [HideInInspector]
    public BuildScript buildScript;
    public int direction = 0;
    public bool rotationSpawnd = false;

    void Awake()
    {
        // Finds the Main Camera and it's BuildScript
        GameObject mainCamera = GameObject.Find("Main Camera");
        buildScript = mainCamera.GetComponent<BuildScript>();

        // Sets the buildings direction to the buildDirection
        direction = buildScript.buildDirection;
        if(!rotationSpawnd) SetRotation();
    }
    public void SetRotation()
    {
        transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, -90 * direction);
        rotationSpawnd = true;
    }
}
