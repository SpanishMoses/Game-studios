using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDrone : MonoBehaviour
{
    public SphereCollider sphere;
    public GameObject Boss;

    public float health;
    public float minHealth;

    public int amountTaken = 1;

    public bool stopped;

    public bool quickTime;

    public Boss boss;

    public AudioSource noise;

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
            transform.RotateAround(Boss.transform.position, Vector3.up, 50 * Time.deltaTime);
        }

        

        if (quickTime == true){
            //StartCoroutine(resetTime());
        }

        /*if (boss.staggered == false){
            maxDeathTime = 5;
        }

        if (stopped == true && boss.staggered == false){
            deathTime += Time.deltaTime;
            if (deathTime >= maxDeathTime){
                deathTime = 0;
                stopped = false;
                health = 5;
                boss.Add(1);
                sphere.enabled = true;
            }
        }*/


        if (boss.resetDrones == true){
            StartCoroutine(enableReset());
        }

            if (boss.staggerTime >= boss.maxStaggerTime)
            {
            Debug.Log("yay");
                stopped = false;
                health = 5;
                boss.Add(1);
                sphere.enabled = true;
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
            noise.Play();
            stopped = true;
            boss.Sub(1);
            sphere.enabled = false;
        }
    }

    IEnumerator enableReset(){
        yield return new WaitForSeconds(0.01f);
        stopped = false;
        health = 20;        
        sphere.enabled = true;
    }

    }
