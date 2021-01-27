using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemySpawner : MonoBehaviour
{

    public Transform spawnPt;

    public GameObject drone;

    public float spawnTime;
    public float maxSpawnTime;

    // Start is called before the first frame update
    void Start()
    {
        maxSpawnTime = Random.Range(25, 30);
    }

    // Update is called once per frame
    void Update()
    {
        spawnTime += Time.deltaTime;
        if (spawnTime >= maxSpawnTime)
        {
           
         Instantiate(drone, spawnPt.position, Quaternion.identity);
            maxSpawnTime = Random.Range(25, 30);
            spawnTime = 0;
        }
        }
}
