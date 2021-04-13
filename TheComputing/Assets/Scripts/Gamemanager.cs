using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class Gamemanager
{
    public static int money = 200, income = 0;
    public static Text moneyText;
    public static List<GameObject> Buildings = new List<GameObject>();

    public static void uppdateText(){
        moneyText.text = "Money: " + money + "\n" + "Income per minute " + income;
    }
}
