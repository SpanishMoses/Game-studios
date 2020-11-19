using System.Collections;
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
        PlayerPrefs.SetInt("Curr_Health", 50);
        PlayerPrefs.SetInt("Current_Score", 0);
        PlayerPrefs.SetInt("Pistol_Ammo", 0);
        PlayerPrefs.SetInt("Shotgun_Ammo", 40);
        PlayerPrefs.SetInt("Machinegun_Ammo", 120);
        PlayerPrefs.SetInt("Grenade_Ammo", 6);
        PlayerPrefs.SetInt("Firework_Ammo", 8);
        PlayerPrefs.SetInt("Curr_Health", 50);

        PlayerPrefs.SetInt("EnableShotgun", 0);
        PlayerPrefs.SetInt("EnableMachineGun", 0);
        PlayerPrefs.SetInt("EnableKnife", 0);
        PlayerPrefs.SetInt("EnableGrenade", 0);
        PlayerPrefs.SetInt("EnableFireWork", 0);
        
    }

    public void arenaEnter(){
        PlayerPrefs.SetInt("Curr_Health", 100);
        PlayerPrefs.SetInt("Current_Score", 0);
        PlayerPrefs.SetInt("Pistol_Ammo", 100);
        PlayerPrefs.SetInt("Shotgun_Ammo", 40);
        PlayerPrefs.SetInt("Machinegun_Ammo", 120);
        PlayerPrefs.SetInt("Grenade_Ammo", 6);
        PlayerPrefs.SetInt("Firework_Ammo", 8);
        PlayerPrefs.SetInt("Curr_Health", 50);

        PlayerPrefs.SetInt("EnableShotgun", 1);
        PlayerPrefs.SetInt("EnableMachineGun", 1);
        PlayerPrefs.SetInt("EnableKnife", 1);
        PlayerPrefs.SetInt("EnableGrenade", 1);
        PlayerPrefs.SetInt("EnableFireWork", 1);
    }
}
