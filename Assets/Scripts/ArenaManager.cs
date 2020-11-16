using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaManager : MonoBehaviour
{
    public GameObject[] enemySpawners;

    public int round;
    public int maxSpawns;
    public int enemiesSpawned;

    // Start is called before the first frame update
    void Start()
    {
        round = 1;
        maxSpawns = 10;
    }

    // Update is called once per frame
    void Update()
    {
        if (enemiesSpawned >= maxSpawns){
            enemiesSpawned = 0;
            StartCoroutine(newRound());
        }

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
    }

    IEnumerator newRound(){
        //set all enemy spawners inactive
        yield return new WaitForSeconds(10);
        //set all enemy spanwers active
        round += 1;
    }
}
