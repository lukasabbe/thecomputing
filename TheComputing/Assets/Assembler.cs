using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Assembler : MonoBehaviour
{
    List<GameObject> motherBoard = new List<GameObject>(1);
    List<GameObject> cpu = new List<GameObject>(1);
    List<GameObject> gpu = new List<GameObject>(2);
    List<GameObject> ram = new List<GameObject>(2);
    List<GameObject> case_ = new List<GameObject>(1);
    List<GameObject> cables = new List<GameObject>(4);
    List<GameObject> harddrive = new List<GameObject>(2);
    List<GameObject> ssd = new List<GameObject>(1);
    List<GameObject> psu = new List<GameObject>(1);

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
        if(( item.GetComponent<Item>().id == 19 || item.GetComponent<Item>().id == 29 || item.GetComponent<Item>().id == 39 )){
            motherBoard.Add(item);
        }

        if(( item.GetComponent<Item>().id == 16 || item.GetComponent<Item>().id == 26 || item.GetComponent<Item>().id == 36 )){
            cpu.Add(item);
        }

        if ((item.GetComponent<Item>().id == 17 || item.GetComponent<Item>().id == 27 || item.GetComponent<Item>().id == 37)) {
            gpu.Add(item);
        }

        if (( item.GetComponent<Item>().id == 21 || item.GetComponent<Item>().id == 31 || item.GetComponent<Item>().id == 41)){
            ram.Add(item);
        }

        if ((item.GetComponent<Item>().id == 14 || item.GetComponent<Item>().id == 24 || item.GetComponent<Item>().id == 34)){
            case_.Add(item);
        }

        if ((item.GetComponent<Item>().id == 23 || item.GetComponent<Item>().id == 33 || item.GetComponent<Item>().id == 34)){
            cables.Add(item);
        }

        if ((item.GetComponent<Item>().id == 18 || item.GetComponent<Item>().id == 28 || item.GetComponent<Item>().id == 38)){
            harddrive.Add(item);
        }

        if ((item.GetComponent<Item>().id == 22 || item.GetComponent<Item>().id == 32 || item.GetComponent<Item>().id == 42)){
            ssd.Add(item);
        }

        if ((item.GetComponent<Item>().id == 23 || item.GetComponent<Item>().id == 33 || item.GetComponent<Item>().id == 34)) {
            psu.Add(item);
        }

        item.SetActive(false);
    }
    void tryToBuildComputer()
    {
        if (motherBoard.Count >= 1 && cpu.Count >= 1 && ram.Count >= 1 && case_.Count >= 1 && cables.Count >= 4 && harddrive.Count >= 1 && ssd.Count >= 1 && psu.Count >= 1)
        {
            GameObject computerInstance = Instantiate(computer, transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity);
            computerInstance.GetComponent<Item>().SellPrice = 1000;

            int quality = 0;

            if (motherBoard[motherBoard.Count - 1].GetComponent<Item>().id == 19){
                quality += 10;
            }else if (motherBoard[motherBoard.Count - 1].GetComponent<Item>().id == 29){
                quality += 30;
            }else if (motherBoard[motherBoard.Count - 1].GetComponent<Item>().id == 39){
                quality += 50;
            }

            if (cpu[cpu.Count - 1].GetComponent<Item>().id == 16){
                quality += 10;
            }else if (cpu[cpu.Count - 1].GetComponent<Item>().id == 26){
                quality += 30;
            }else if (cpu[cpu.Count - 1].GetComponent<Item>().id == 36){
                quality += 50;
            }

            if (gpu[gpu.Count - 1].GetComponent<Item>().id == 17){
                quality += 10;
            }else if (gpu[gpu.Count - 1].GetComponent<Item>().id == 27){
                quality += 30;
            }else if (gpu[gpu.Count - 1].GetComponent<Item>().id == 37){
                quality += 50;
            }

            if (ram[ram.Count - 1].GetComponent<Item>().id == 21){
                quality += 10;
            }else if (ram[ram.Count - 1].GetComponent<Item>().id == 31){
                quality += 30;
            }else if (ram[ram.Count - 1].GetComponent<Item>().id == 41){
                quality += 50;
            }

            if (case_[case_.Count - 1].GetComponent<Item>().id == 14){
                quality += 10;
            }else if (case_[case_.Count - 1].GetComponent<Item>().id == 24){
                quality += 30;
            }else if (case_[case_.Count - 1].GetComponent<Item>().id == 34){
                quality += 50;
            }

            if (cables[cables.Count - 1].GetComponent<Item>().id == 13){
                quality += 10;
            }else if (cables[cables.Count - 1].GetComponent<Item>().id == 23){
                quality += 30;
            }else if (cables[cables.Count - 1].GetComponent<Item>().id == 33){
                quality += 50;
            }

            if (cables[cables.Count - 1].GetComponent<Item>().id == 13){
                quality += 10;
            }else if (cables[cables.Count - 1].GetComponent<Item>().id == 23){
                quality += 30;
            }else if (cables[cables.Count - 1].GetComponent<Item>().id == 33){
                quality += 50;
            }

            motherBoard.RemoveAt(motherBoard.Count - 1);
            cpu.RemoveAt(cpu.Count - 1);
            ram.RemoveAt(ram.Count - 1);
            case_.RemoveAt(case_.Count - 1);
            cables.RemoveRange(0, 4);
            harddrive.RemoveAt(harddrive.Count - 1);
            ssd.RemoveAt(ssd.Count - 1);
            psu.RemoveAt(psu.Count - 1);
        }
    }
}
