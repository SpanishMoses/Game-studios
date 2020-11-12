using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaEnemySpawn : MonoBehaviour
{
    public Transform spawnPt;

    public GameObject basic;
    public GameObject drone;
    public GameObject tank;
    public GameObject charge;
    public GameObject explosion;

    public int enemyNum;

    public float spawnTime;
    public float maxSpawnTime;

    // Start is called before the first frame update
    void Start()
    {
        maxSpawnTime = Random.Range(10, 20);
        randomNum();
    }

    // Update is called once per frame
    void Update()
    {
        spawnTime += Time.deltaTime;
        if (spawnTime >= maxSpawnTime){
            if (enemyNum == 1){
                Instantiate(basic, spawnPt.position, Quaternion.identity);
            }
            if (enemyNum == 2)
            {
                Instantiate(drone, spawnPt.position, Quaternion.identity);
            }
            if (enemyNum == 3)
            {
                Instantiate(tank, spawnPt.position, Quaternion.identity);
            }
            if (enemyNum == 4)
            {
                Instantiate(charge, spawnPt.position, Quaternion.identity);
            }
            if (enemyNum == 5)
            {
                Instantiate(explosion, spawnPt.position, Quaternion.identity);
            }
            if (enemyNum == 6)
            {
                Instantiate(basic, spawnPt.position, Quaternion.identity);
            }
            maxSpawnTime = Random.Range(10, 20);
            spawnTime = 0;
            randomNum();
        }
    }

    void randomNum(){
        enemyNum = Random.Range(1, 6);
    }
}
