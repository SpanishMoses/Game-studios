using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] door;
    public GameObject[] enemies;

    public bool activated;

    public int enemiesSpawned;

    public BoxCollider box;

    public bool spawnPortal;
    public GameObject portal;

    // Start is called before the first frame update
    void Start()
    {
        activated = false;
        for (int i = 0; i < enemies.Length; i++){
            enemies[i].SetActive(false);
        }
        enemiesSpawned = enemies.Length;
    }

    // Update is called once per frame
    void Update()
    {
        if (enemiesSpawned <= 0){
            for (int i = 0; i < door.Length; i++){
                door[i].SetActive(false);
                if (spawnPortal == true){
                    portal.SetActive(true);
                }
            }
            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && activated == false){
            for (int i = 0; i < enemies.Length; i++)
            {
                enemies[i].SetActive(true);
                activated = true;
                box.enabled = false;
                //Destroy(gameObject);
            }
            for (int i = 0; i < door.Length; i++)
            {
                door[i].SetActive(true);
            }
        }
    }

    public void deductEnemy(int deduct){
        enemiesSpawned -= deduct;
    }
}
