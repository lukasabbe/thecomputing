using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Discord;
public class DiscordManger : MonoBehaviour
{
    public Discord.Discord discord;
    public Activity activity;
    ActivityManager am;
    private void Awake()
    {
        StartCoroutine(waitUp());
    }
    private void OnApplicationQuit()
    {;
        am.ClearActivity(result => {
            if (result == Discord.Result.Ok)
            {
                Debug.Log("Cleared");
            }
            else
            {
                Debug.Log("failed");
            }
        });
        discord.Dispose();
    }
    private void Start()
    {
        discord = new Discord.Discord(825723739874000906, (System.UInt16)Discord.CreateFlags.Default);
        discord.RunCallbacks();
        am = discord.GetActivityManager();
    }
    private void Update()
    {
        discord.RunCallbacks();
        activity = new Discord.Activity
        {
            State = "Is collecting",
            Details = "Money: " + Gamemanager.money.ToString() + "\nIncome " + Gamemanager.income.ToString()
        };

    }
    IEnumerator waitUp()
    {
        yield return new WaitForSeconds(10);
        am.UpdateActivity(activity, (res) =>
        {
            if (res == Discord.Result.Ok)
            {
                //Debug.Log("uppdate ac");
            }
        });
        StartCoroutine(waitUp());
    }
}

