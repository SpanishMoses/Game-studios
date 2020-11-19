using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaManager : MonoBehaviour
{
    public GameObject[] enemySpawners;

    public GameObject[] enemies;

    public int round;
    public int maxSpawns;
    public int enemiesSpawned;

    public bool currentlyRound;
    public bool spawnersInactive;

    // Start is called before the first frame update
    void Start()
    {
        round = 1;
        maxSpawns = 10;
        currentlyRound = true;
        spawnersInactive = false;
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

        if (maxSpawns == 0 & spawnersInactive == true && currentlyRound == true){
            currentlyRound = false;
            StartCoroutine(newRound());
        }
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
    }
}
