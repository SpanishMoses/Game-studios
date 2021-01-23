using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointGiver : MonoBehaviour
{
    public BoxCollider collid;
    public PointManager mainPoint;
    public int targetPoints;
    public EnemyHealth health;
    public bool isHead;
    public AudioSource headShot;
    public bool hasSpecial;

    private void Awake()
    {
        mainPoint = GameObject.FindGameObjectWithTag("PointManager").GetComponent<PointManager>();
    }

    public void GivePoint(int points) {
        mainPoint.totalPoints += points;
        mainPoint.ShakeIt();
    }
        public void TakeDamage(float amount)
        {
            health.health -= amount;
        if (isHead == true){
            health.health -= amount * 2;
            headShot.Play();
        }
        if (health.health <= 0f && health.partOfArray == false)
        {
            Debug.Log("dead");
            //StartCoroutine(startNormalDeath());
            health.animator.SetTrigger("IsDead");
            health.enemyMove.navMeshAgent.speed = 0;
            health.enemyShoot.enabled = false;
            collid.enabled = false;
            health.death.clip = health.deathNoise;
            health.death.Play();
            if (hasSpecial == true){
                health.special.SetTrigger("IsDead");
            }
        }
        if (health.health <= 0 && health.partOfArray == true)
        {
            //StartCoroutine(startDeath());
            health.animator.SetTrigger("IsDead");
            Debug.Log("started");
            health.enemyMove.navMeshAgent.speed = 0;
            health.enemyShoot.enabled = false;
            collid.enabled = false;
            health.death.clip = health.deathNoise;
            health.death.Play();
            if (hasSpecial == true)
            {
                health.special.SetTrigger("IsDead");
            }
        }
    }
    }

