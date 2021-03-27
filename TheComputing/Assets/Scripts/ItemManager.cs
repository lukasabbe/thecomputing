using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour{
    public LayerMask conveyorLayer, itemLayer;
    public Rigidbody2D rBody;
    private void Update(){
        if (Physics2D.OverlapCircle(transform.position, 0.2f, conveyorLayer)){
            GameObject currentConveyor = Physics2D.OverlapCircle(transform.position, 0.2f, conveyorLayer).gameObject;
            ConveyorBeltManager currentConveyorScript = currentConveyor.GetComponent<ConveyorBeltManager>();

            if (currentConveyorScript.isOn){
                Vector2 velocity = Vector2.zero;
                switch (currentConveyorScript.direction){
                    case 0:
                        if (!Physics2D.OverlapBox(transform.position + new Vector3(0, 0.4f, 0), new Vector2(0.2f, 0.2f), 0, itemLayer)){
                            if (transform.position.x > currentConveyor.transform.position.x) velocity = new Vector2(-1, 1);
                            else velocity = new Vector2(1, 1);
                        }
                        break;
                    case 1:
                        if (!Physics2D.OverlapBox(transform.position + new Vector3(0.4f, 0, 0), new Vector2(0.2f, 0.2f), 0, itemLayer)){
                            if (transform.position.y > currentConveyor.transform.position.y) velocity = new Vector2(1, -1);
                            else velocity = new Vector2(1, 1);
                        }
                        break;
                    case 2:
                        if (!Physics2D.OverlapBox(transform.position + new Vector3(0, -0.4f, 0), new Vector2(0.2f, 0.2f), 0, itemLayer)){
                            if (transform.position.x > currentConveyor.transform.position.x) velocity = new Vector2(-1, -1);
                            else velocity = new Vector2(1, -1);
                        }
                        break;
                    case 3:
                        if (!Physics2D.OverlapBox(transform.position + new Vector3(-0.4f, 0, 0), new Vector2(0.2f, 0.2f), 0, itemLayer)){
                            if (transform.position.y > currentConveyor.transform.position.y) velocity = new Vector2(-1, -1);
                            else velocity = new Vector2(-1, 1);
                        }
                        break;
                }
                rBody.velocity = velocity;
            }
        }
        else rBody.velocity = Vector2.zero;
    }
}
