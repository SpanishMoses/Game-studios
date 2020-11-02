﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public void SinglePlayerbutton(string newGameLevel)
    {
        Time.timeScale = 1f;
        StartCoroutine(loadlevel());
        IEnumerator loadlevel()
        {
            yield return new WaitForSeconds(1);
            SceneManager.LoadScene(newGameLevel);
        }
    }

    public void resetScore(){
        PlayerPrefs.SetInt("Current_Score", 0);
        PlayerPrefs.SetInt("Pistol_Ammo", 0);
        PlayerPrefs.SetInt("Shotgun_Ammo", 40);
        PlayerPrefs.SetInt("Machinegun_Ammo", 120);
        PlayerPrefs.SetInt("Grenade_Ammo", 6);
        PlayerPrefs.SetInt("Firework_Ammo", 8);
        PlayerPrefs.SetInt("Curr_Health", 50);
    }
}
