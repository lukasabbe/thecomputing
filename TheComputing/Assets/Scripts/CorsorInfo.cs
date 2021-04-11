using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorsorInfo : MonoBehaviour
{
    [HideInInspector]
    public BuildScript buildScript;
    public GameObject cursorInfoTextBox;
    [HideInInspector]
    public GameObject text;
    

    void Start()
    {
        buildScript = GetComponent<BuildScript>();
        Instantiate(cursorInfoTextBox);
        text = GameObject.Find("cursorInfoText");

    }
    void Update()
    {
        CursorInfo();
    }
    void CursorInfo()
    {
        Collider2D[] hitColliders = Physics2D.OverlapBoxAll(buildScript.cursorPosition(), new Vector2(0.2f, 0.2f), 0); // Creates a list of colliders in the tile the cursor is over


        
        GameObject infoObject = null;
        
        string itemName = null;

        foreach (Collider2D hitCollider in hitColliders)
        {
            if (hitCollider.gameObject.tag != "Item") // id1 is the id of the last item in the list.
            {
                infoObject = hitCollider.gameObject;
                
            }
            if (hitCollider.gameObject.tag == "Item") // id1 is the id of the last item in the list.
            {
                infoObject = hitCollider.gameObject;
                itemName = hitCollider.gameObject.GetComponent<Item>().ItemName;
            }
        }
        text.SetActive(false);
        if (itemName != null)
        {
            text.SetActive(true);
            text.GetComponent<RectTransform>().position = new Vector2((buildScript.cursorPosition().x - transform.position.x) * 48 + 515, (buildScript.cursorPosition().y - transform.position.y) * 48 + 245);
            text.GetComponent<UnityEngine.UI.Text>().text = itemName;
        }
    }
}