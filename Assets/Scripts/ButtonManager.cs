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
        PlayerPrefs.SetInt("Current_Score", 0);
        PlayerPrefs.GetInt("Pistol_Ammo", 0);
        PlayerPrefs.GetInt("Shotgun_Ammo", 40);
        PlayerPrefs.GetInt("Machinegun_Ammo", 120);
        PlayerPrefs.GetInt("Grenade_Ammo", 6);
        PlayerPrefs.GetInt("Firework_Ammo", 8);
    }
}
