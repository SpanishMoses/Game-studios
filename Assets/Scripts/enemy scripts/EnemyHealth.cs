using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    //hit scan help from https://www.youtube.com/watch?v=THnivyG0Mvo&ab_channel=Brackeys

    public bool partOfArray;

    public float health;
    public float minHealth;

    public int amountTaken = 1;

    public GameObject blood;
    public GameObject parent;

    public BoxCollider collid;

    //public ArrayedEnemy array;
    public EnemySpawner enemySpawn;

    private void Update()
    {
        if (health <= minHealth){
            health = minHealth;
        }
    }

    public void TakeDamage(float amount){
        health -= amount;
        Instantiate(blood, parent.transform.position, Quaternion.identity);
        if (health <= 0f && partOfArray == false)
        {
            Debug.Log("dead");
            Destroy(gameObject);
        }
            if (health <= 0 && partOfArray == true){
            StartCoroutine(startDeath());
            Debug.Log("started");
        }
    }

    IEnumerator startDeath(){
        collid.enabled = false;
        yield return new WaitForSeconds(1f);
        enemySpawn.deductEnemy(amountTaken);
        Destroy(gameObject);
    }
}
