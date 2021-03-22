using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class Gamemanager
{
    public static int Money = 0;
    public static Text t;
    public static void uppdateText()
    {
        t.text = "Money: " + Money;
    }
}
