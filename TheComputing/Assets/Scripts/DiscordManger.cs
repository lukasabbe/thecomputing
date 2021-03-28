using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Discord;
public class DiscordManger : MonoBehaviour
{
    public Discord.Discord discord;
    public Activity activity;
    ActivityManager am;

    private void Start()
    {
        discord = new Discord.Discord(825723739874000906, (System.UInt16)Discord.CreateFlags.Default);
        am = discord.GetActivityManager();
        activity = new Discord.Activity
        {
            State = "Stats: ",
            Details = ""
        };
        am.UpdateActivity(activity, (res) =>
        {
            if(res == Discord.Result.Ok)
            {
                Debug.Log("Evryting is very fine");
            }
        });
    }
    private void Update()
    {
        discord.RunCallbacks();
        activity.Details = "money: " + Gamemanager.money.ToString();
        am.UpdateActivity(activity, (res) =>
        {
            if (res == Discord.Result.Ok)
            {
                Debug.Log("Evryting is very fine");
            }
        });
    }
}
