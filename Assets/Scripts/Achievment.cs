using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Steamworks;

public class Achievment : MonoBehaviour
{
    public bool tutorialArea;

    public PlayerMovement player;
    public TextManager text;

    //for enemy amounts
    public int bearKills;
    public int unicornKills;
    public int droneKills;
    public int explodeKills;
    public int pandaKills;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        if (tutorialArea == true){
            text = GameObject.FindGameObjectWithTag("Dia").GetComponent<TextManager>();
        }
    }

    // Update is called once per frame
    void Update()
    {

        //test achievment thing
        if (bearKills >= 5)
        {
            if (!SteamManager.Initialized) { return; }
            SteamUserStats.SetAchievement("Exterminator_Part_One");
            SteamUserStats.StoreStats();
        }

        if (unicornKills >= 5){
            if (!SteamManager.Initialized) { return; }
            SteamUserStats.SetAchievement("Exterminator_Part_Two");
            SteamUserStats.StoreStats();
        }

        if (droneKills >= 5){
            if (!SteamManager.Initialized) { return; }
            SteamUserStats.SetAchievement("Exterminator_Part_Three");
            SteamUserStats.StoreStats();
        }

        if (explodeKills >= 5)
        {
            if (!SteamManager.Initialized) { return; }
            SteamUserStats.SetAchievement("Exterminator_Part_Four");
            SteamUserStats.StoreStats();
        }

        if (pandaKills >= 5)
        {
            if (!SteamManager.Initialized) { return; }
            SteamUserStats.SetAchievement("Exterminator_Part_Five");
            SteamUserStats.StoreStats();
        }

        if (Input.GetKeyDown(KeyCode.L)){
            if (!SteamManager.Initialized) { return; }
            SteamUserStats.ResetAllStats(true);
        }
    }
}
