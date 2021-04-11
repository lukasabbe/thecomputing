using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBeltManager : MonoBehaviour
{
    public float speedMultiplier = 1;
    public bool isOn = true;

    [HideInInspector] public int up = 0, right = 1, down = 2, left = 3;
    public int direction = 0;

    private void Awake(){
        int buildDirection = Camera.main.GetComponent<BuildScript>().buildDirection;

        direction = buildDirection;
        transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, -90 * buildDirection);
    }
}