using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    public GameObject[] enemies;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < enemies.Length; i++){
            enemies[i].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player"){
            for (int i = 0; i < enemies.Length; i++)
            {
                enemies[i].SetActive(true);
                Destroy(gameObject);
            }
        }
    }
}
