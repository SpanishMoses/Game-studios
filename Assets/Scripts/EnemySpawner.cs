using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject door;
    public GameObject[] enemies;

    public bool activated;

    public int enemiesSpawned;

    public BoxCollider box;

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
            door.SetActive(false);
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
        }
    }
}
