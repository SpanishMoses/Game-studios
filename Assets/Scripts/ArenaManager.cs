using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ArenaManager : MonoBehaviour
{
    public GameObject[] enemySpawners;

    public GameObject[] enemies;

    public int round;
    public int maxSpawns;
    public int enemiesSpawned;

    public bool currentlyRound;
    public bool spawnersInactive;

    public TMP_Text roundText;
    public GameObject randomTextObj;
    public TMP_Text randomText;

    // Start is called before the first frame update
    void Start()
    {
        round = 1;
        maxSpawns = 10;
        currentlyRound = true;
        spawnersInactive = false;
        randomTextObj.SetActive(false);
        //enemies = GameObject.FindGameObjectsWithTag("Enemy");
    }

    // Update is called once per frame
    void Update()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");

        /*if (enemiesSpawned >= maxSpawns){
            enemiesSpawned = 0;
            //StartCoroutine(newRound());
        }*/

        if (round == 5){
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

        if (enemiesSpawned >= maxSpawns){
            for (int i = 0; i < enemySpawners.Length; i++){
                enemySpawners[i].SetActive(false);
                spawnersInactive = true;
            }
        }

        if (enemiesSpawned == 0 & spawnersInactive == true && currentlyRound == true){
            currentlyRound = false;
            StartCoroutine(newRound());
        }

        roundText.text = "Round: " + round;
    }

    IEnumerator newRound(){
        //set all enemy spawners inactive
        yield return new WaitForSeconds(10);
        //set all enemy spanwers active
        round += 1;
        spawnersInactive = false;
        currentlyRound = true;
        for (int i = 0; i < enemySpawners.Length; i++)
        {
            enemySpawners[i].SetActive(true);

        }
        yield return new WaitForSeconds(0.5f);
        randomTextObj.SetActive(true);
        randomText.text = "Get Ready";
        yield return new WaitForSeconds(1f);
        randomText.text = "SLAUGHTER";
        yield return new WaitForSeconds(1f);
        randomTextObj.SetActive(false);
    }
}
