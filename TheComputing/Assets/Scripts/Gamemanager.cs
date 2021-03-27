using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class Gamemanager
{
    public static int money = 0, income;
    public static Text moneyText;

    public static void uppdateText(){
        moneyText.text = "Money: " + money + "\n" + "Income per minute " + income;
    }
}
