using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tunnel : MonoBehaviour{
    int direction = 0;
    const int Up = 0, Down = 2, Right = 1, Left = 3;

    int tunnelRange = 5;
    public LayerMask tunnelLayer;
    public LayerMask itemLayer;

    private void Awake(){
        sh(Camera.main.GetComponent<BuildScript>().buildDirection);
    }

    public void sh(int dir)
    {
        direction = dir;

        for (int i = 1; i < tunnelRange; i++)
        {
            switch (direction)
            {
                case Up:
                    if (Physics2D.OverlapCircle(transform.position + new Vector3(0, i, 0), 0.2f, tunnelLayer))
                    {
                        Physics2D.OverlapCircle(transform.position + new Vector3(0, i, 0), 0.2f, tunnelLayer).GetComponent<Tunnel>().tunnel();
                    }
                    break;
                case Down:
                    if (Physics2D.OverlapCircle(transform.position + new Vector3(0, -i, 0), 0.2f, tunnelLayer))
                    {
                        Physics2D.OverlapCircle(transform.position + new Vector3(0, -i, 0), 0.2f, tunnelLayer).GetComponent<Tunnel>().tunnel();
                    }
                    break;
                case Left:
                    if (Physics2D.OverlapCircle(transform.position + new Vector3(i, 0, 0), 0.2f, tunnelLayer))
                    {
                        Physics2D.OverlapCircle(transform.position + new Vector3(i, 0, 0), 0.2f, tunnelLayer).GetComponent<Tunnel>().tunnel();
                    }
                    break;
                case Right:
                    if (Physics2D.OverlapCircle(transform.position + new Vector3(-i, 0, 0), 0.2f, tunnelLayer))
                    {
                        Physics2D.OverlapCircle(transform.position + new Vector3(-i, 0, 0), 0.2f, tunnelLayer).GetComponent<Tunnel>().tunnel();
                    }
                    break;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision){
        tunnel();
    }
    public void tunnel(){
        Vector3 itemCheckOffset = Vector3.zero;
        switch (direction){
            case Up:
                itemCheckOffset = new Vector3(0, -0.25f, 0);
                break;
            case Down:
                itemCheckOffset = new Vector3(0, 0.25f, 0);
                break;
            case Left:
                itemCheckOffset = new Vector3(0.25f, 0, 0);
                break;
            case Right:
                itemCheckOffset = new Vector3(-0.25f, 0, 0);
                break;
        }

        for (int i = 1; i < tunnelRange; i++){
            GameObject tunnelToTransportTo = null; 
            GameObject itemToTransport = null;

            Vector3 tunnelCheckPosition = Vector3.zero;
            switch (direction){
                case Up:
                    tunnelCheckPosition = new Vector3(0, i, 0);
                    break;
                case Down:
                    tunnelCheckPosition = new Vector3(0, -i, 0);
                    break;
                case Left:
                    tunnelCheckPosition = new Vector3(-i, 0, 0);
                    break;
                case Right:
                    tunnelCheckPosition = new Vector3(i, 0, 0);
                    break;
            }

            if (Physics2D.OverlapCircle(transform.position + tunnelCheckPosition, 0.2f, tunnelLayer)){ //Checkar alla rutorna framför i tunnelns range för en annan tunnel i motsatt rotation
                tunnelToTransportTo = Physics2D.OverlapCircle(transform.position + tunnelCheckPosition, 0.2f, tunnelLayer).gameObject;
                if (Physics2D.OverlapBox(transform.position + itemCheckOffset, new Vector2(0.5f, 0.5f), 0, itemLayer)){
                    itemToTransport = Physics2D.OverlapBox(transform.position + itemCheckOffset, new Vector2(0.5f, 0.5f), 0, itemLayer).gameObject;
                    StartCoroutine(transportItem(itemToTransport, tunnelToTransportTo, i));
                }
            }
        }
    }
    IEnumerator transportItem(GameObject itemToTransport, GameObject tunnelToTransportTo, float time){
        Vector3 itemDepositOffset = Vector3.zero;
        switch (direction){
            case Up:
                itemDepositOffset = new Vector3(0, 1, 0);
                break;
            case Down:
                itemDepositOffset = new Vector3(0, -1, 0);
                break;
            case Left:
                itemDepositOffset = new Vector3(-1, 0, 0);
                break;
            case Right:
                itemDepositOffset = new Vector3(1, 0, 0);
                break;
        }

        if (itemToTransport != null) itemToTransport.SetActive(false);
        yield return new WaitForSeconds(time * 1.2f);
        if (itemToTransport != null && tunnelToTransportTo != null){
            itemToTransport.SetActive(true);
            itemToTransport.transform.position = tunnelToTransportTo.transform.position + itemDepositOffset;
        }
    }
}
