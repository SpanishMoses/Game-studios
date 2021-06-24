using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public float positionX;
    public float positionY;
    public float positionZ;

    public AudioSource noise;
    public AudioClip hover;
    public AudioClip enterSound;

    public void SinglePlayerbutton(string newGameLevel)
    {
        Time.timeScale = 1f;
        StartCoroutine(loadlevel());
        IEnumerator loadlevel()
        {
            yield return new WaitForSeconds(1);
            SceneManager.LoadScene(newGameLevel);
            PlayerPrefs.SetFloat("CheckPointX", positionX);
            PlayerPrefs.SetFloat("CheckPointY", positionY);
            PlayerPrefs.SetFloat("CheckPointZ", positionZ);
            PlayerPrefs.SetInt("PriorH", 50);
            PlayerPrefs.SetInt("PriorP", 0);
            PlayerPrefs.SetInt("PriorS", 25);
            PlayerPrefs.SetInt("PriorM", 120);
            PlayerPrefs.SetInt("PriorG", 6);
            PlayerPrefs.SetInt("PriorF", 8);
            PlayerPrefs.SetInt("PriorScore", 0);
            PlayerPrefs.SetFloat("Sense", 4);
            PlayerPrefs.SetInt("GOD", 0);
        }
    }

    public void resetScore(){
        PlayerPrefs.SetInt("Curr_Health", 50);
        PlayerPrefs.SetInt("Current_Score", 0);
        PlayerPrefs.SetInt("Pistol_Ammo", 0);
        PlayerPrefs.SetInt("Shotgun_Ammo", 25);
        PlayerPrefs.SetInt("Machinegun_Ammo", 120);
        PlayerPrefs.SetInt("Grenade_Ammo", 6);
        PlayerPrefs.SetInt("Firework_Ammo", 8);
        PlayerPrefs.SetInt("Curr_Health", 50);

        PlayerPrefs.SetInt("EnableShotgun", 0);
        PlayerPrefs.SetInt("EnableMachineGun", 0);
        PlayerPrefs.SetInt("EnableKnife", 0);
        PlayerPrefs.SetInt("EnableGrenade", 0);
        PlayerPrefs.SetInt("EnableFireWork", 0);

        PlayerPrefs.SetFloat("TIMER", 0);
        PlayerPrefs.SetFloat("SECONDS", 0);
        PlayerPrefs.SetFloat("MINUTES", 0);
        PlayerPrefs.SetFloat("HOURS", 0);

    }

    public void arenaEnter(string arena)
    {
        Time.timeScale = 1f;
        StartCoroutine(loadArena());
        IEnumerator loadArena()
        {
            yield return new WaitForSeconds(1);
            SceneManager.LoadScene(arena);
            PlayerPrefs.SetInt("Curr_Health", 100);
            PlayerPrefs.SetInt("Current_Score", 0);
            PlayerPrefs.SetInt("Pistol_Ammo", 100);
            PlayerPrefs.SetInt("PriorP", 100);
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
            PlayerPrefs.SetFloat("Sense", 4);
            PlayerPrefs.SetInt("Current_Round", 1);
            PlayerPrefs.SetInt("Current_RoundV2", 1);
        }
    }

    public void hoverNow(){
        noise.clip = hover;
        noise.Play();
    }

    public void nowClick(){
        noise.clip = enterSound;
        noise.Play();
    }
}
