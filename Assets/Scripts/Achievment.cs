using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Steamworks;

public class Achievment : MonoBehaviour
{
    public bool tutorialArea;

    public PlayerMovement player;
    public TextManager text;

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
        if (player.jumpNum == 5)
        {
            if (!SteamManager.Initialized) { return; }
            SteamUserStats.SetAchievement("Exterminator_Part_One");
            SteamUserStats.StoreStats();
        }

        if (text.enable == true){
            if (!SteamManager.Initialized) { return; }
            SteamUserStats.SetAchievement("Exterminator_Part_Two");
            SteamUserStats.StoreStats();
        }
    }
}
