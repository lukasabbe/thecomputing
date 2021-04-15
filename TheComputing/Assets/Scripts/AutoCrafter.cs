using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoCrafter : MonoBehaviour
{
    /*

    ----------Item ids----------

    ScrapMetal      : 0
    TrashPlastic    : 1

    Plastic         : 2
    Polymer         : 3
    TrashCopper     : 4
    TrashGold       : 5
    TrashIron       : 6
    TrashSilver     : 7

    Copper          : 8
    Gold            : 9
    Iron            : 10
    Rubber          : 11
    Silver          : 12

    Low quality Cables           : 13
    Low quality Case             : 14
    Low quality CircuitBoard     : 15
    Low quality CPU              : 16
    Low quality GPU              : 17
    Low quality Harddrive        : 18
    Low quality Motherboard      : 19
    Low quality PSU              : 20
    Low quality RAM              : 21
    Low quality SSD              : 22

    Cables                       : 23
    Case                         : 24
    CircuitBoard                 : 25
    CPU                          : 26
    GPU                          : 27
    Harddrive                    : 28
    Motherboard                  : 29
    PSU                          : 30
    RAM                          : 31
    SSD                          : 32

    High quality Cables          : 33
    High quality Case            : 34
    High quality CircuitBoard    : 35
    High quality CPU             : 36
    High quality GPU             : 37
    High quality Harddrive       : 38
    High quality Motherboard     : 39
    High quality PSU             : 40
    High quality RAM             : 41
    High quality SSD             : 42

    Computer                     : 43

    ----------Item ids---------- 

    */

    BuildScript buildScript;
    AutoCrafterUI ui;
    public LayerMask itemLayer;

    Vector3 input1, input2, output;
    const int Up = 0, Down = 2, Right = 1, Left = 3;

    public List<slot> slots = new List<slot>(4);
    public List<recipie> recipies = new List<recipie>();

    public int selectedRecipeIndex = 0;

    public List<GameObject> itemsCrafted = new List<GameObject>();

    public RecipieManager[] recipieObj;

    public float craftTime = 1f;
    const float craftTimeReset = 1f;

    slot emptySlot0 = new slot();
    slot emptySlot1 = new slot();
    slot emptySlot2 = new slot();
    slot emptySlot3 = new slot();
    private void Start(){
        ui = GetComponent<AutoCrafterUI>();
        buildScript = Camera.main.GetComponent<BuildScript>();

        switch (buildScript.buildDirection)
        {
            case (Up):
                input1 = new Vector3(transform.position.x - 0.5f, transform.position.y, transform.position.z);
                input2 = new Vector3(transform.position.x + 0.5f, transform.position.y, transform.position.z);
                output = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
                break;
            case (Down):
                input1 = new Vector3(transform.position.x + 0.5f, transform.position.y, transform.position.z);
                input2 = new Vector3(transform.position.x - 0.5f, transform.position.y, transform.position.z);
                output = new Vector3(transform.position.x, transform.position.y - 1, transform.position.z);
                break;
            case (Left):
                input1 = new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z);
                input2 = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
                output = new Vector3(transform.position.x - 1, transform.position.y, transform.position.z);
                break;
            case (Right):
                input1 = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
                input2 = new Vector3(transform.position.x, transform.position.y+ 0.5f, transform.position.z);
                output = new Vector3(transform.position.x + 1, transform.position.y, transform.position.z);
                break;
        }

        slots.Add(emptySlot0);
        slots.Add(emptySlot1);
        slots.Add(emptySlot2);
        slots.Add(emptySlot3);

        for(int i = 0; i < recipieObj.Length; i++){
            recipie recipieTemp = new recipie();
            if(recipieObj[i].itemIds.Length == recipieObj[i].itemAmounts.Length){
                recipieTemp.itemIds = recipieObj[i].itemIds;
                recipieTemp.itemAmounts = recipieObj[i].itemAmounts;
                recipieTemp.uniqueItems = recipieObj[i].itemIds.Length;
                recipieTemp.item = recipieObj[i].item;
                recipies.Add(recipieTemp);
            }
            else{
                Debug.LogError("Trying to add recipie with arrays of different sizes. Make sure the 'ItemId' and 'ItemAmount' arrays have the same length.");
            }
        }
    }

    private void Update(){
        float itemCheckRadius = 0.05f;
        if (Physics2D.OverlapCircle(input1, itemCheckRadius, itemLayer)){
            Debug.Log("INPUT1 picked up an item");
            GameObject itemToAdd = Physics2D.OverlapCircle(input1, itemCheckRadius, itemLayer).gameObject;
            addItemToSlot(itemToAdd);
        }
        if(Physics2D.OverlapCircle(input2, itemCheckRadius, itemLayer)){
            Debug.Log("INPUT2 picked up an item");
            GameObject itemToAdd = Physics2D.OverlapCircle(input2, itemCheckRadius, itemLayer).gameObject;
            addItemToSlot(itemToAdd);
        }

        if (craftTime > 0) craftTime -= Time.deltaTime;
        if(itemsCrafted.Count > 0 && !Physics2D.OverlapCircle(output, 0.4f, itemLayer) && craftTime <= 0){
            craftTime = craftTimeReset;
            Instantiate(itemsCrafted[itemsCrafted.Count - 1], output, Quaternion.identity);
            itemsCrafted.RemoveAt(itemsCrafted.Count - 1);
        }
    }

    public void addItemToSlot(GameObject item){
        Debug.Log("Trying to add " + item.GetComponent<Item>().ItemName);
        if(item != null){
            bool hasBeenAdded = false;
            for (int i = 0; i < slots.Count; i++){
                if(slots[i].item != null){ //kollar om slotten är tom
                    if (slots[i].item.GetComponent<Item>().id == item.GetComponent<Item>().id && !hasBeenAdded){ //lägger till items i stacken om itemet som tas upp matchar itemet i slotten
                        hasBeenAdded = true;
                        slots[i].itemsInStack++;
                        ui.UpdateUI();
                        Debug.Log("item: " + item.GetComponent<Item>().ItemName + " id: " + item.GetComponent<Item>().id);
                        item.SetActive(false);
                    }
                }
            }
            if (!hasBeenAdded)
                for (int i = 0; i < slots.Count; i++)
                    if (slots[i].item == null && !hasBeenAdded){ //fyller tomma slotts 
                        hasBeenAdded = true;
                        slots[i].item = item;
                        slots[i].itemsInStack++;
                        ui.UpdateUI();
                        Debug.Log("item: " + item.GetComponent<Item>().ItemName + "id: " + item.GetComponent<Item>().id);
                        item.SetActive(false);
                    }
            if (slots[0].item != null && slots[1].item != null) tryToCraft();
        }
    }
    List<GameObject> itemsToDelete = new List<GameObject>();
    public void tryToCraft(){
        Debug.Log("TRYING TO CRAFT");
        bool[] hasItemsOnSlot = new bool[] { false, false, false, false }; //Varje crafting slot har en check som skapas här
        for(int i = 0; i < recipies[selectedRecipeIndex].uniqueItems; i++){ //Loopar igenom alla recipies
            for(int i1 = 0; i1 < slots.Count; i1++){ //Loopar igenom alla crafterns slotts 
                if(slots[i1].item != null){ //Kollar så att slottens item inte är null
                    if (recipies[selectedRecipeIndex].itemIds[i] == slots[i1].item.GetComponent<Item>().id && recipies[selectedRecipeIndex].itemAmounts[i] <= slots[i1].itemsInStack){ //Kollar om recipies item matchar slottens item och mängd
                        Debug.Log("slot" + (i1 + 1) + ": " + slots[i1].item);
                        hasItemsOnSlot[i] = true;
                    }
                }
            }
        }

        bool hasRequiredItems = true;
        for (int i = 0; i < recipies[selectedRecipeIndex].uniqueItems; i++) //Kollar om alla slots hade rätt item och mängd
            if (!hasItemsOnSlot[i]) hasRequiredItems = false; 

        if (hasRequiredItems){
            for (int i = 0; i < recipies[selectedRecipeIndex].uniqueItems; i++){
                for (int i1 = 0; i1 < slots.Count; i1++){
                    if(slots[i1].item != null){
                        if (recipies[selectedRecipeIndex].itemIds[i] == slots[i1].item.GetComponent<Item>().id){ //hittar itemsen som användes i recipien
                            slots[i1].itemsInStack -= recipies[selectedRecipeIndex].itemAmounts[i]; //Tar bort itemsen som användes i crafting recipien
                            itemsToDelete.Add(slots[i1].item);
                            if (slots[i1].itemsInStack <= 0){
                                switch (i1){ //emptyar slotten om den inte har några items i sig
                                    case (0):
                                        slots[0] = emptySlot0;
                                        break;
                                    case (1):
                                        slots[1] = emptySlot1;
                                        break;
                                    case (2):
                                        slots[2] = emptySlot2;
                                        break;
                                    case (3):
                                        slots[3] = emptySlot3;
                                        break;
                                }
                            }
                        }
                    }
                }
            }
            for (int i = 0; i < recipieObj[selectedRecipeIndex].itemsCraftedPerTime; i++)
                itemsCrafted.Add(recipies[selectedRecipeIndex].item); //lägger till itemet som craftade i en lista

            for (int i = 0; i < itemsToDelete.Count; i++) // loopar igenom alla items som användes i craftingen och tar bort dem
                Destroy(itemsToDelete[i]);
            itemsToDelete.Clear();

            ui.UpdateUI();
        }
    }
    public void SetCraftingIndex(int index){
        Debug.Log("Index changed: " + index);

        itemsCrafted.Clear();
        slots.Clear();

        slots.Add(emptySlot0);
        slots.Add(emptySlot1);
        slots.Add(emptySlot2);
        slots.Add(emptySlot3);

        ui.UpdateUI();

        selectedRecipeIndex = index;
    }
    public class slot
    {
        public int itemsInStack = 0;
        public GameObject item = null;
    }
    public class recipie
    {
        public int uniqueItems = 0;
        public int[] itemIds;
        public int[] itemAmounts;

        public GameObject item;
    }

    /*
  public GameObject dropLocation, leftInput, rightInput;

  public List<GameObject> outputItems = new List<GameObject>(); // List of all craftable items

  void Update()
  {
      Crafter();
  }
  void Crafter()
  {
      Collider2D[] leftSide = Physics2D.OverlapBoxAll(leftInput.transform.position, new Vector2(0.2f, 0.2f), 0); // Kollar på 1 item på den vänstra sidan
      Collider2D[] rightSide = Physics2D.OverlapBoxAll(rightInput.transform.position, new Vector2(0.2f, 0.2f), 0); // Kollar på 1 item på den högra sidan

      GameObject itemA = null, itemB = null;
      int id1 = 10000, id2 = 10000;

      foreach (Collider2D a in leftSide)
      {
          if (a.gameObject.tag == "Item") // id1 is the id of the last item in the list.
          {
              itemA = a.gameObject;
              id1 = a.gameObject.GetComponent<Item>().id;
          }
      }
      foreach (Collider2D b in rightSide)
      {
          if (b.gameObject.tag == "Item") // id2 is the id of the last item in the list.
          {
              itemB = b.gameObject;
              id2 = b.gameObject.GetComponent<Item>().id;
          }
      }
      if (EmptyInFront())
      {
          // All recipes for crafting
          if (id1 == 1 && id2 == 5 || id2 == 1 && id1 == 5) // 1 = gold, 5 = plastic   output 0 = circuit board
          {
              // Drop wanted item and delete required items.
              GameObject craftedItem = Instantiate(outputItems[0]);
              craftedItem.transform.position = dropLocation.transform.position;
              Destroy(itemA);
              Destroy(itemB);
          }
          // TEST RECIPE för test
          else if (id1 == 7 && id2 == 2 || id2 == 7 && id1 == 2) // 8 = trash plastic, 0 = scrap metal   output 
          {
              // Drop wanted item and delete required items.
              GameObject craftedItem = Instantiate(outputItems[1]);
              craftedItem.transform.position = dropLocation.transform.position;
              Destroy(itemA);
              Destroy(itemB);
          }
          // TEST RECIPE för test
          else if (id1 == 0 && id2 == 8 || id2 == 0 && id1 == 8) // 8 = trash plastic, 0 = scrap metal   output 
          {
              // Drop wanted item and delete required items.
              GameObject craftedItem = Instantiate(outputItems[2]);
              craftedItem.transform.position = dropLocation.transform.position;
              Destroy(itemA);
              Destroy(itemB);
          }
      }
  }
  bool EmptyInFront() // Makes sure it only drops when its empty at the drop location, to avoid many items in the same place
  {
      Collider2D[] hitColliders = Physics2D.OverlapBoxAll(dropLocation.transform.position, new Vector2(0.25f, 0.25f), 0);

      if (hitColliders == null) return false;
      foreach (var hitCollider in hitColliders)
      {
          if (hitCollider.gameObject.tag == "Item")
          {
              return false;
          }
      }
      return true;
  }
  */
}
