using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    //hit scan help from https://www.youtube.com/watch?v=THnivyG0Mvo&ab_channel=Brackeys

    public float health;

    public GameObject blood;
    public GameObject parent;

    public void TakeDamage(float amount){
        health -= amount;
        Instantiate(blood, parent.transform.position, Quaternion.identity);
        if (health <= 0f){
            Debug.Log("dead");
            Destroy(gameObject);
        }
    }
}
