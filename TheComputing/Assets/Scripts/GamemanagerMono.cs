﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GamemanagerMono : MonoBehaviour{
    public Canvas[] popup = new Canvas[2];

    private void Awake(){
        StartCoroutine(calculateIncome());
    }
    IEnumerator calculateIncome(){
        int currentMoney = Gamemanager.money;

        yield return new WaitForSeconds(60);

        Gamemanager.income = Gamemanager.money - currentMoney;

        StartCoroutine(calculateIncome());
    }
}
