using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ArenaManagerV2 : MonoBehaviour
{
    public GameObject[] enemySpawners;

    public GameObject[] enemies;

    public int round;
    private int highRound;
    public int maxSpawns;
    public int enemiesSpawned;

    public bool currentlyRound;
    public bool spawnersInactive;

    public TMP_Text roundText;
    public GameObject randomTextObj;
    public TMP_Text randomText;

    public PlayerMovement player;

    public AudioSource musicMan;
    public AudioClip track1;
    public AudioClip track2;
    public AudioClip track3;
    public AudioClip track4;
    public AudioClip track5;
    public int songNum;

    public AudioSource announcer;
    public AudioSource ambiance;
    public AudioClip getReady;
    public AudioClip firstDing;
    public AudioClip secondDing;

    // Start is called before the first frame update
    void Start()
    {
        round = 1;
        maxSpawns = 10;
        currentlyRound = true;
        spawnersInactive = false;
        randomTextObj.SetActive(false);
        round = PlayerPrefs.GetInt("Current_RoundV2", 1);
        highRound = PlayerPrefs.GetInt("high_waveV2", 0);
        //enemies = GameObject.FindGameObjectsWithTag("Enemy");
        chooseSong();
        songNum = PlayerPrefs.GetInt("Song");
        if (songNum == 1)
        {
            musicMan.clip = track4;
            musicMan.Play();
        }

        if (songNum == 2)
        {
            musicMan.clip = track1;
            musicMan.Play();
        }

        if (songNum == 3)
        {
            musicMan.clip = track2;
            musicMan.Play();
        }

        if (songNum == 4)
        {
            musicMan.clip = track3;
            musicMan.Play();
        }

        if (songNum == 5)
        {
            musicMan.clip = track5;
            musicMan.Play();
        }
    }

    // Update is called once per frame
    void Update()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");

        /*if (enemiesSpawned >= maxSpawns){
            enemiesSpawned = 0;
            //StartCoroutine(newRound());
        }*/

        if (round == 5)
        {
            maxSpawns = 20;
        }
        if (round == 10)
        {
            maxSpawns = 30;
        }
        if (round == 20)
        {
            maxSpawns = 40;
        }
        if (round == 30)
        {
            maxSpawns = 50;
        }

        enemiesSpawned = GameObject.FindGameObjectsWithTag("Enemy").Length;

        if (enemiesSpawned >= maxSpawns)
        {
            for (int i = 0; i < enemySpawners.Length; i++)
            {
                enemySpawners[i].SetActive(false);
                spawnersInactive = true;
            }
        }

        if (enemiesSpawned == 0 & spawnersInactive == true && currentlyRound == true)
        {
            currentlyRound = false;
            StartCoroutine(newRound());
        }

        roundText.text = "Round: " + round;

        /*if (player.health <= 0){
            SceneManager.LoadScene("MainMenuLoadScreen");
        }*/

        PlayerPrefs.SetInt("Current_RoundV2", round);
        if (round > highRound)
        {
            highRound = round;
            PlayerPrefs.SetInt("high_waveV2", highRound);
        }
    }

    IEnumerator newRound()
    {
        //set all enemy spawners inactive
        yield return new WaitForSeconds(5);
        //set all enemy spanwers active
        round += 1;
        spawnersInactive = false;
        currentlyRound = true;
        for (int i = 0; i < enemySpawners.Length; i++)
        {
            enemySpawners[i].SetActive(true);

        }
        yield return new WaitForSeconds(0.5f);
        announcer.clip = getReady;
        announcer.Play();
        ambiance.clip = firstDing;
        ambiance.Play();
        randomTextObj.SetActive(true);
        randomText.text = "Get Ready";
        yield return new WaitForSeconds(1.5f);
        ambiance.clip = secondDing;
        ambiance.Play();
        randomText.text = "SLAUGHTER";
        yield return new WaitForSeconds(1f);
        randomTextObj.SetActive(false);
    }

    public void chooseSong()
    {
        /*songNum = Random.Range(1, 8);
        if (songNum == 1)
        {
            musicMan.clip = track1;
            musicMan.Play();
        }
        if (songNum == 2)
        {
            musicMan.clip = track1;
            musicMan.Play();
        }
        if (songNum == 3)
        {
            musicMan.clip = track2;
            musicMan.Play();
        }
        if (songNum == 4)
        {
            musicMan.clip = track2;
            musicMan.Play();
        }
        if (songNum == 5)
        {
            musicMan.clip = track3;
            musicMan.Play();
        }
        if (songNum == 6)
        {
            musicMan.clip = track3;
            musicMan.Play();
        }
        if (songNum == 7)
        {
            musicMan.clip = track4;
            musicMan.Play();
        }
        if (songNum == 8)
        {
            musicMan.clip = track4;
            musicMan.Play();
        }*/
    }
}
