using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AutoCrafterUI : MonoBehaviour{
    AutoCrafter autoCrafterScript;

    public Image[] inventoryImg;
    public Text[] inventoryTxt;

    public Image selectedRecipeImg;
    public Text selectedRecipeTxt;

    public GameObject panel, panelParent;

    public Button button;
    private void Start(){
        autoCrafterScript = GetComponent<AutoCrafter>();

        Debug.Log(autoCrafterScript.recipieObj.Length);
        CreateItemList();
    }
    public void UpdateUI(){
        for (int i = 0; i < 4; i++){
            if (autoCrafterScript.slots[i].item != null){
                inventoryImg[i].sprite = autoCrafterScript.slots[i].item.GetComponent<SpriteRenderer>().sprite;
                inventoryImg[i].color = autoCrafterScript.slots[i].item.GetComponent<SpriteRenderer>().color;
                if(inventoryImg[i].color.a == 0) inventoryImg[i].color += new Color(0, 0, 0, 1);

                inventoryTxt[i].text = autoCrafterScript.slots[i].itemsInStack.ToString();
            }else{
                inventoryImg[i].color += new Color(0, 0, 0, -1);
            }

            selectedRecipeImg.sprite = autoCrafterScript.recipies[autoCrafterScript.selectedRecipeIndex].item.GetComponent<SpriteRenderer>().sprite;
            selectedRecipeImg.color = autoCrafterScript.recipies[autoCrafterScript.selectedRecipeIndex].item.GetComponent<SpriteRenderer>().color;
            selectedRecipeTxt.text = autoCrafterScript.itemsCrafted.Count.ToString();
        }
    }
    void CreateItemList(){
        for (int i = 0; i < autoCrafterScript.recipieObj.Length; i++) {
            GameObject newPanel = Instantiate(panel, Vector3.zero, Quaternion.Euler(0, 0, 90), panelParent.transform);

            newPanel.transform.localPosition = panelParent.transform.localPosition + new Vector3(0, -(118 * i) - 60, 0);
            newPanel.transform.localRotation = Quaternion.Euler(0, 0, 0);

            newPanel.transform.GetChild(0).GetComponent<Text>().text = autoCrafterScript.recipieObj[i].item.GetComponent<Item>().ItemName;
            newPanel.transform.GetChild(1).GetComponent<Text>().text = autoCrafterScript.recipieObj[i].item.GetComponent<Item>().ItemDescription;

            newPanel.transform.GetChild(2).GetComponent<Image>().sprite = autoCrafterScript.recipieObj[i].item.GetComponent<SpriteRenderer>().sprite;
            newPanel.transform.GetChild(2).GetComponent<Image>().color = autoCrafterScript.recipieObj[i].item.GetComponent<SpriteRenderer>().color;

            newPanel.transform.GetChild(4).GetComponent<Image>().sprite = GetItem(autoCrafterScript.recipieObj[i].itemIds[0]).GetComponent<SpriteRenderer>().sprite;
            newPanel.transform.GetChild(4).GetComponent<Image>().color = GetItem(autoCrafterScript.recipieObj[i].itemIds[0]).GetComponent<SpriteRenderer>().color;
            newPanel.transform.GetChild(9).GetComponent<Text>().text = autoCrafterScript.recipieObj[i].itemAmounts[0].ToString();

            newPanel.transform.GetChild(5).GetComponent<Image>().sprite = GetItem(autoCrafterScript.recipieObj[i].itemIds[1]).GetComponent<SpriteRenderer>().sprite;
            newPanel.transform.GetChild(5).GetComponent<Image>().color = GetItem(autoCrafterScript.recipieObj[i].itemIds[1]).GetComponent<SpriteRenderer>().color;
            newPanel.transform.GetChild(10).GetComponent<Text>().text = autoCrafterScript.recipieObj[i].itemAmounts[1].ToString();

            if (autoCrafterScript.recipieObj[i].itemIds.Length > 2){
                newPanel.transform.GetChild(6).GetComponent<Image>().sprite = GetItem(autoCrafterScript.recipieObj[i].itemIds[2]).GetComponent<SpriteRenderer>().sprite;
                newPanel.transform.GetChild(6).GetComponent<Image>().color = GetItem(autoCrafterScript.recipieObj[i].itemIds[2]).GetComponent<SpriteRenderer>().color;
                newPanel.transform.GetChild(11).GetComponent<Text>().text = autoCrafterScript.recipieObj[i].itemAmounts[2].ToString();
            } else {
                newPanel.transform.GetChild(6).GetComponent<Image>().color += new Color(0, 0, 0, -1);
                newPanel.transform.GetChild(11).GetComponent<Text>().color += new Color(0, 0, 0, -1);
            }

            if (autoCrafterScript.recipieObj[i].itemIds.Length > 3){
                newPanel.transform.GetChild(7).GetComponent<Image>().sprite = GetItem(autoCrafterScript.recipieObj[i].itemIds[3]).GetComponent<SpriteRenderer>().sprite;
                newPanel.transform.GetChild(7).GetComponent<Image>().color = GetItem(autoCrafterScript.recipieObj[i].itemIds[3]).GetComponent<SpriteRenderer>().color;
                newPanel.transform.GetChild(12).GetComponent<Text>().text = autoCrafterScript.recipieObj[i].itemAmounts[3].ToString();
            }
            else{
                newPanel.transform.GetChild(7).GetComponent<Image>().color += new Color(0, 0, 0, -1);
                newPanel.transform.GetChild(12).GetComponent<Text>().color += new Color(0, 0, 0, -1);
            }

            newPanel.transform.GetChild(8).GetComponent<Text>().text = autoCrafterScript.recipieObj[i].itemsCraftedPerTime.ToString();

            int index = i;
            newPanel.transform.GetChild(13).GetComponent<Button>().onClick.AddListener(delegate { autoCrafterScript.SetCraftingIndex(index); });
            newPanel.transform.GetChild(13).GetComponent<Button>().onClick.AddListener(delegate { UpdateUI(); });
        }
        UpdateUI();

        gameObject.transform.GetChild(1).gameObject.SetActive(false);
    }
    public GameObject[] listOfItems = new GameObject[42];
    public GameObject GetItem(int index){
        return listOfItems[index];
    }
}
