using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    //hit scan help from https://www.youtube.com/watch?v=THnivyG0Mvo&ab_channel=Brackeys

    public float health;

    public void TakeDamage(float amount){
        health -= amount;
        if (health <= 0f){
            Debug.Log("dead");
        }
    }
}
