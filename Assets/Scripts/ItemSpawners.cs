using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawners : MonoBehaviour
{
    public Transform spawnPos;

    public GameObject health;
    public GameObject ammo;
    public GameObject rocketAmmo;
    public GameObject grenadeAmmo;

    public int itemNum;

    public float spawnTime;
    public float maxSpawnTime;

    public bool beginSpawn;

    // Start is called before the first frame update
    void Start()
    {
        itemNum = Random.Range(0, 11);
        beginSpawn = true;
        maxSpawnTime = Random.Range(10, 15);
    }

    // Update is called once per frame
    void Update()
    {
        if (beginSpawn == true){
            spawnTime += Time.deltaTime;
            if (spawnTime >= maxSpawnTime){
                if (itemNum == 0){
                    Instantiate(health, spawnPos.position, Quaternion.identity);
                }
                if (itemNum == 1)
                {
                    Instantiate(health, spawnPos.position, Quaternion.identity);
                }
                if (itemNum == 2)
                {
                    Instantiate(ammo, spawnPos.position, Quaternion.identity);
                }
                if (itemNum == 3)
                {
                    Instantiate(ammo, spawnPos.position, Quaternion.identity);
                }
                if (itemNum == 4)
                {
                    Instantiate(ammo, spawnPos.position, Quaternion.identity);
                }
                if (itemNum == 5)
                {
                    Instantiate(rocketAmmo, spawnPos.position, Quaternion.identity);
                }
                if (itemNum == 6)
                {
                    Instantiate(rocketAmmo, spawnPos.position, Quaternion.identity);
                }
                if (itemNum == 7)
                {
                    Instantiate(grenadeAmmo, spawnPos.position, Quaternion.identity);
                }
                if (itemNum == 8)
                {
                    Instantiate(grenadeAmmo, spawnPos.position, Quaternion.identity);
                }
                if (itemNum == 9)
                {
                    Instantiate(health, spawnPos.position, Quaternion.identity);
                }
                if (itemNum == 10)
                {
                    Instantiate(ammo, spawnPos.position, Quaternion.identity);
                }
                if (itemNum == 11)
                {
                    Instantiate(ammo, spawnPos.position, Quaternion.identity);
                }
                spawnTime = 0;
                itemNum = Random.Range(0, 11);
                maxSpawnTime = Random.Range(10, 20);
                beginSpawn = false;
            }
        }
    }
}
