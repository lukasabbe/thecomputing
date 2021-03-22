using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seller : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Item")
        {
            Debug.Log("Sold " + collision.GetComponent<Item>().ItemName + " for " + collision.GetComponent<Item>().SellPrice + " dollars");
            Destroy(collision.gameObject);
        }
    }
}
