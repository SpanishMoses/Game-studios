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

    public int pistolKill;
    public int shotGunKill;
    public int rifleKill;
    public int knifeKill;
    public int grenadeKill;
    public int fireWorkKill;

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
        if (bearKills >= 100)
        {
            if (!SteamManager.Initialized) { return; }
            SteamUserStats.SetAchievement("Exterminator_Part_One");
            SteamUserStats.StoreStats();
        }

        if (unicornKills >= 100){
            if (!SteamManager.Initialized) { return; }
            SteamUserStats.SetAchievement("Exterminator_Part_Two");
            SteamUserStats.StoreStats();
        }

        if (droneKills >= 100){
            if (!SteamManager.Initialized) { return; }
            SteamUserStats.SetAchievement("Exterminator_Part_Three");
            SteamUserStats.StoreStats();
        }

        if (explodeKills >= 100)
        {
            if (!SteamManager.Initialized) { return; }
            SteamUserStats.SetAchievement("Exterminator_Part_Four");
            SteamUserStats.StoreStats();
        }

        if (pandaKills >= 100)
        {
            if (!SteamManager.Initialized) { return; }
            SteamUserStats.SetAchievement("Exterminator_Part_Five");
            SteamUserStats.StoreStats();
        }

        if (pistolKill >= 1 && shotGunKill >= 1 && rifleKill >= 1 && knifeKill >= 1 && grenadeKill >= 1 && fireWorkKill >= 1){
            if (!SteamManager.Initialized) { return; }
            SteamUserStats.SetAchievement("Weapon_Specialist");
            SteamUserStats.StoreStats();
        }

        if (Input.GetKeyDown(KeyCode.L)){
            if (!SteamManager.Initialized) { return; }
            SteamUserStats.ResetAllStats(true);
        }
    }
}
