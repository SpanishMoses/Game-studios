using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDrone : MonoBehaviour
{

    public GameObject Boss;

    public float health;
    public float minHealth;

    public int amountTaken = 1;

    public bool stopped;

    public float deathTime;
    public float maxDeathTime;

    // Start is called before the first frame update
    void Start()
    {
        stopped = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (stopped == false)
        {
            transform.RotateAround(Boss.transform.position, Vector3.up, 20 * Time.deltaTime);
        }

        if (stopped == true){
            deathTime += Time.deltaTime;
            if (deathTime >= maxDeathTime){
                deathTime = 0;
                stopped = false;
                health = 5;
            }
        }

        if (health <= minHealth)
        {
            health = minHealth;
        }
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0f)
        {
            stopped = true;
        }
    }
    }
