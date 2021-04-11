using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GamemanagerMono : MonoBehaviour{
    private void Awake(){
        StartCoroutine(calculateIncome());
    }
    IEnumerator calculateIncome(){
        int currentMoney = Gamemanager.money;

        yield return new WaitForSeconds(30);

        Gamemanager.income = (Gamemanager.money - currentMoney) * 2;

        StartCoroutine(calculateIncome());
    }
}
