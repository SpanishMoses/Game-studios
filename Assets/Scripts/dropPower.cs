using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class dropPower : MonoBehaviour
{
    public GameObject enemy;
    public EnemyHealth health;
    public int itemNum;
    public GameObject speed;
    public GameObject insta;
    public GameObject max;
    public GameObject infinite;
    public dropPower drop;

    // Start is called before the first frame update
    void Start()
    {
        itemNum = Random.Range(1, 20);
    }

    // Update is called once per frame
    void Update()
    {
        if (health.health <= 0 && SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Arena") && itemNum == 1){
            Instantiate(speed, enemy.transform.position, Quaternion.identity);
            drop.enabled = false;
        }

        if (health.health <= 0 && SceneManager.GetActiveScene() == SceneManager.GetSceneByName("ArenaV2") && itemNum == 1)
        {
            Instantiate(speed, enemy.transform.position, Quaternion.identity);
            drop.enabled = false;
        }
        if (health.health <= 0 && SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Arena") && itemNum == 7)
        {
            Instantiate(insta, enemy.transform.position, Quaternion.identity);
            drop.enabled = false;
        }

        if (health.health <= 0 && SceneManager.GetActiveScene() == SceneManager.GetSceneByName("ArenaV2") && itemNum == 7)
        {
            Instantiate(insta, enemy.transform.position, Quaternion.identity);
            drop.enabled = false;
        }
        if (health.health <= 0 && SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Arena") && itemNum == 14)
        {
            Instantiate(max, enemy.transform.position, Quaternion.identity);
            drop.enabled = false;
        }

        if (health.health <= 0 && SceneManager.GetActiveScene() == SceneManager.GetSceneByName("ArenaV2") && itemNum == 14)
        {
            Instantiate(max, enemy.transform.position, Quaternion.identity);
            drop.enabled = false;
        }
        if (health.health <= 0 && SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Arena") && itemNum == 20)
        {
            Instantiate(infinite, enemy.transform.position, Quaternion.identity);
            drop.enabled = false;
        }

        if (health.health <= 0 && SceneManager.GetActiveScene() == SceneManager.GetSceneByName("ArenaV2") && itemNum == 20)
        {
            Instantiate(infinite, enemy.transform.position, Quaternion.identity);
            drop.enabled = false;
        }
    }
}
