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
        bearKills = PlayerPrefs.GetInt("B_Kills", 0);
        unicornKills = PlayerPrefs.GetInt("U_Kills", 0);
        droneKills = PlayerPrefs.GetInt("D_Kills", 0);
        explodeKills = PlayerPrefs.GetInt("E_Kills", 0);
        pandaKills = PlayerPrefs.GetInt("P_Kills", 0);

        pistolKill = PlayerPrefs.GetInt("P_Gun_Kills", 0);
        shotGunKill = PlayerPrefs.GetInt("S_Gun_Kills", 0);
        rifleKill = PlayerPrefs.GetInt("R_Gun_Kills", 0);
        knifeKill = PlayerPrefs.GetInt("K_Gun_Kills", 0);
        grenadeKill = PlayerPrefs.GetInt("G_Gun_Kills", 0);
        fireWorkKill = PlayerPrefs.GetInt("F_Gun_Kills", 0);
    }

    // Update is called once per frame
    void Update()
    {

        PlayerPrefs.SetInt("B_Kills", bearKills);
        PlayerPrefs.SetInt("U_Kills", unicornKills);
        PlayerPrefs.SetInt("D_Kills", droneKills);
        PlayerPrefs.SetInt("E_Kills", explodeKills);
        PlayerPrefs.SetInt("P_Kills", pandaKills);

        PlayerPrefs.SetInt("P_Gun_Kills", pistolKill);
        PlayerPrefs.SetInt("S_Gun_Kills", shotGunKill);
        PlayerPrefs.SetInt("R_Gun_Kills", rifleKill);
        PlayerPrefs.SetInt("K_Gun_Kills", knifeKill);
        PlayerPrefs.SetInt("G_Gun_Kills", grenadeKill);
        PlayerPrefs.SetInt("F_Gun_Kills", fireWorkKill);

        //test achievment thing
        if (bearKills >= 100)
        {
            if (!SteamManager.Initialized) { return; }
            SteamUserStats.SetAchievement("Exterminator_Part_One");
            SteamUserStats.StoreStats();
            PlayerPrefs.SetInt("ACH_1", 1);
        }

        if (unicornKills >= 100){
            if (!SteamManager.Initialized) { return; }
            SteamUserStats.SetAchievement("Exterminator_Part_Two");
            SteamUserStats.StoreStats();
            PlayerPrefs.SetInt("ACH_2", 1);
        }

        if (droneKills >= 100){
            if (!SteamManager.Initialized) { return; }
            SteamUserStats.SetAchievement("Exterminator_Part_Three");
            SteamUserStats.StoreStats();
            PlayerPrefs.SetInt("ACH_3", 1);
        }

        if (explodeKills >= 100)
        {
            if (!SteamManager.Initialized) { return; }
            SteamUserStats.SetAchievement("Exterminator_Part_Four");
            SteamUserStats.StoreStats();
            PlayerPrefs.SetInt("ACH_4", 1);
        }

        if (pandaKills >= 100)
        {
            if (!SteamManager.Initialized) { return; }
            SteamUserStats.SetAchievement("Exterminator_Part_Five");
            SteamUserStats.StoreStats();
            PlayerPrefs.SetInt("ACH_5", 1);
        }

        if (bearKills >= 100 && unicornKills >= 100 && droneKills >= 100 && explodeKills >= 100 && pandaKills >= 100){
            if (!SteamManager.Initialized) { return; }
            SteamUserStats.SetAchievement("Ultimate_Slayer");
            SteamUserStats.StoreStats();
            PlayerPrefs.SetInt("ACH_6", 1);
        }

        if (pistolKill >= 1 && shotGunKill >= 1 && rifleKill >= 1 && knifeKill >= 1 && grenadeKill >= 1 && fireWorkKill >= 1){
            if (!SteamManager.Initialized) { return; }
            SteamUserStats.SetAchievement("Weapon_Specialist");
            SteamUserStats.StoreStats();
            PlayerPrefs.SetInt("ACH_21", 1);
        }

        if (Input.GetKeyDown(KeyCode.L)){
            if (!SteamManager.Initialized) { return; }
            SteamUserStats.ResetAllStats(true);
        }
    }
}
