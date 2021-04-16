using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combiner : MonoBehaviour
{
    BuildScript buildScript;
    Vector3 input1, input2, input3, output;

    public LayerMask itemLayer;

    const int Up = 0, Down = 2, Right = 1, Left = 3;

    List<GameObject> itemsInStorage = new List<GameObject>();
    void Start(){
        buildScript = Camera.main.GetComponent<BuildScript>();

        switch (buildScript.buildDirection)
        {
            case (Up):
                input1 = new Vector3(transform.position.x - 0.5f, transform.position.y, transform.position.z);
                input2 = new Vector3(transform.position.x + 0.5f, transform.position.y, transform.position.z);
                input3 = new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z);
                output = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
                break;
            case (Down):
                input1 = new Vector3(transform.position.x + 0.5f, transform.position.y, transform.position.z);
                input2 = new Vector3(transform.position.x - 0.5f, transform.position.y, transform.position.z);
                input3 = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
                output = new Vector3(transform.position.x, transform.position.y - 1, transform.position.z);
                break;
            case (Left):
                input1 = new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z);
                input2 = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
                input3 = new Vector3(transform.position.x + 0.5f, transform.position.y, transform.position.z);
                output = new Vector3(transform.position.x - 1, transform.position.y, transform.position.z);
                break;
            case (Right):
                input1 = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
                input2 = new Vector3(transform.position.x, transform.position.y + -0.5f, transform.position.z);
                input3 = new Vector3(transform.position.x - 0.5f, transform.position.y, transform.position.z);
                output = new Vector3(transform.position.x + 1, transform.position.y, transform.position.z);
                break;
        }
    }

    void Update()
    {
        float itemCheckRadius = 0.05f;
        if (Physics2D.OverlapCircle(input1, itemCheckRadius, itemLayer)){
            GameObject itemToAdd = Physics2D.OverlapCircle(input1, itemCheckRadius, itemLayer).gameObject;
            StartCoroutine(Combine(itemToAdd));
        }
        if (Physics2D.OverlapCircle(input2, itemCheckRadius, itemLayer)){
            GameObject itemToAdd = Physics2D.OverlapCircle(input2, itemCheckRadius, itemLayer).gameObject;
            StartCoroutine(Combine(itemToAdd));
        }
        if (Physics2D.OverlapCircle(input3, itemCheckRadius, itemLayer)){
            GameObject itemToAdd = Physics2D.OverlapCircle(input3, itemCheckRadius, itemLayer).gameObject;
            StartCoroutine(Combine(itemToAdd));
        }

        if (itemsInStorage.Count > 0 && !Physics2D.OverlapCircle(output, 0.4f, itemLayer)){
            GameObject itemInstance = Instantiate(itemsInStorage[0], output, Quaternion.identity);
            itemInstance.SetActive(true);
            itemsInStorage.RemoveAt(0);
        }
    }
    IEnumerator Combine(GameObject item){
        item.SetActive(false);
        yield return new WaitForSeconds(1);
        itemsInStorage.Add(item);
    }
}
