using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Assembler : MonoBehaviour
{
    bool hasMotherboard = false, hasCpu = false, hasGpu = false, hasRam0 = false, hasRam1 = false;
    bool hasCase = false;
    bool hasCables0 = false, hasCables1 = false, hasCables2 = false, hasCables3 = false;
    bool hasHarddrive = false, hasSSD = false;
    bool hasPSU = false;

    int hasMotherboardi = 0, hasCpui = 0, hasGpui = 0, hasRam0i = 0, hasRam1i = 0;
    int hasCasei = 0;
    int hasCables0i = 0, hasCables1i = 0, hasCables2i = 0, hasCables3i = 0;
    int hasHarddrivei = 0, hasSSDi = 0;
    int hasPSUi = 0;

    GameObject motherBoard, cpu, gpu, ram0, ram1;
    GameObject case_;
    GameObject cables0, cables1, cables2, cables3;
    GameObject harddrive, ssd;
    GameObject psu;

    public GameObject computer;

    public LayerMask itemLayer;

    public GameObject[] compuratt = new GameObject[0];

    private void Update(){
        if (Physics2D.OverlapCircle(transform.position + new Vector3(-0.5f, 0, 0), 0.05f, itemLayer)) tryToAttachPart(Physics2D.OverlapCircle(transform.position + new Vector3(-0.5f, 0, 0), 0.05f, itemLayer).gameObject);
        if (Physics2D.OverlapCircle(transform.position + new Vector3(0.5f, 0, 0), 0.05f, itemLayer)) tryToAttachPart(Physics2D.OverlapCircle(transform.position + new Vector3(0.5f, 0, 0), 0.05f, itemLayer).gameObject);

        if (Input.GetKeyDown(KeyCode.U))
        {
            for(int i = 0; i < compuratt.Length; i++)
            {
                Instantiate(compuratt[i],Camera.main.ScreenToWorldPoint(Input.mousePosition), Quaternion.identity);
            }
        }
        tryToBuildComputer();
    }
    void tryToAttachPart(GameObject item){
        Debug.Log("afhopo hjklh jkalf hesfhbkfbhenwfbn km,fbn v cmvcvxcmvmxn 1 111111111111111111111111");

        if(!hasMotherboard && ( item.GetComponent<Item>().id == 19 || item.GetComponent<Item>().id == 29 || item.GetComponent<Item>().id == 39 )){
            hasMotherboard = true;
            hasMotherboardi++;
            motherBoard = item;
        }

        if(!hasCpu && ( item.GetComponent<Item>().id == 16 || item.GetComponent<Item>().id == 26 || item.GetComponent<Item>().id == 36 )){
            hasCpui++;
            cpu = item;
        }

        if (!hasGpu && (item.GetComponent<Item>().id == 16 || item.GetComponent<Item>().id == 26 || item.GetComponent<Item>().id == 36)) {
            hasGpui++;
            cpu = item;
        }

        if (!hasRam0 && ( item.GetComponent<Item>().id == 17 || item.GetComponent<Item>().id == 27 || item.GetComponent<Item>().id == 37)){
            hasRam0i++;
            gpu = item;
        }
        if ((!hasRam1 && hasRam0) && (item.GetComponent<Item>().id == 21 || item.GetComponent<Item>().id == 31 || item.GetComponent<Item>().id == 41)){
            hasRam1i++;
            ram1 = item;
        }

        if (!hasCase && (item.GetComponent<Item>().id == 14 || item.GetComponent<Item>().id == 24 || item.GetComponent<Item>().id == 34)){
            hasCasei++;
            case_ = item;
        }

        if (!hasCables0 && (item.GetComponent<Item>().id == 23 || item.GetComponent<Item>().id == 33 || item.GetComponent<Item>().id == 34)){
            hasCables0i++;
            cables0 = item;
        }
        if ((!hasCables1 && hasCables0) && (item.GetComponent<Item>().id == 23 || item.GetComponent<Item>().id == 33 || item.GetComponent<Item>().id == 34)){
            hasCables1i++;
            cables1 = item;
        }
        if ((!hasCables2 && hasCables1) && (item.GetComponent<Item>().id == 23 || item.GetComponent<Item>().id == 33 || item.GetComponent<Item>().id == 34)){
            hasCables2i++;
            cables2 = item;
        }
        if ((!hasCables3 && hasCables2)&& (item.GetComponent<Item>().id == 23 || item.GetComponent<Item>().id == 33 || item.GetComponent<Item>().id == 34)){
            hasCables3i++;
            cables3 = item;
        }

        if (!hasHarddrive && (item.GetComponent<Item>().id == 18 || item.GetComponent<Item>().id == 28 || item.GetComponent<Item>().id == 38)){
            hasHarddrivei++;
            hasHarddrive = item;
        }
        if (!hasSSD && (item.GetComponent<Item>().id == 22 || item.GetComponent<Item>().id == 32 || item.GetComponent<Item>().id == 42)){
            hasSSDi++;
            ssd = item;
        }

        if (!hasPSU && (item.GetComponent<Item>().id == 23 || item.GetComponent<Item>().id == 33 || item.GetComponent<Item>().id == 34)) {
            hasPSUi++;
            psu = item;
        }

        item.SetActive(false);
    }
    void tryToBuildComputer()
    {
        if (hasMotherboardi > 0) hasMotherboard = true;
        if (hasCpui > 0) hasCpu = true;
        if (hasRam0i > 0) hasRam0 = true;
        if (hasRam1i > 0) hasRam1 = true;
        if (hasCasei > 0) hasCase = true;
        if (hasCables0i > 0) hasCables0 = true;
        if (hasCables1i > 0) hasCables1 = true;
        if (hasCables2i > 0) hasCables2 = true;
        if (hasCables3i > 0) hasCables3 = true;
        if (hasHarddrivei > 0) hasHarddrive = true;
        if (hasSSDi > 0) hasSSD = true;
        if (hasPSUi > 0) hasPSU = true;

        if (hasMotherboard && hasCpu && (hasRam0 || (hasRam0 && hasRam1)) && hasCase && hasCables0 && hasCables1 && hasCables2 && hasCables3 && hasHarddrive && hasSSD && hasPSU)
        {
            Instantiate(computer, transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity);

            hasMotherboardi--;
            hasCpui--;
            hasGpui--;
            hasRam0i--;
            hasRam1i--;
            hasCasei--;
            hasCables0i--;
            hasCables1i--;
            hasCables2i--;
            hasCables3i--;
            hasHarddrivei--;
            hasSSDi--;
            hasPSUi--;

            if(hasMotherboardi <= 0) hasMotherboard = false;
            if(hasCpui <= 0) hasCpu = false;
            if (hasRam0i <= 0) hasRam0 = false;
            if (hasRam1i <= 0) hasRam1 = false;
            if (hasCasei <= 0) hasCase = false;
            if (hasCables0i <= 0) hasCables0 = false;
            if (hasCables1i <= 0) hasCables1 = false;
            if (hasCables2i <= 0) hasCables2 = false;
            if (hasCables3i <= 0) hasCables3 = false;
            if (hasHarddrivei <= 0) hasHarddrive = false;
            if (hasSSDi <= 0) hasSSD = false;
            if (hasPSUi <= 0) hasPSU = false;
        }
    }
}
