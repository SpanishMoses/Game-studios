﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ExplodingEnemy : MonoBehaviour
{
    public float playerDist;
    public float blowUpDist;
    public float speed;

    public float radius;
    public int damage;

    public GameObject spawnEffect;

    private Transform playerLoc;

    public NavMeshAgent navMeshAgent;

    private Animator animator;

    public bool locActive;

    public bool deductExplode;

    public EnemySpawner enemySpawn;
    public EnemyHealth health;

    public GameObject effect;

    public AudioSource fuse;

    // Start is called before the first frame update
    void Start()
    {
        playerLoc = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        navMeshAgent = this.GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();


    }

    void Awake()
    {
        Instantiate(spawnEffect, transform.position, transform.rotation);
    }

    private void Update()
    {
        if (Vector2.Distance(transform.position, playerLoc.transform.position) < playerDist)
        {
            locActive = true;
            fuse.Play();
        }

        if (Vector2.Distance(transform.position, playerLoc.transform.position) < blowUpDist)
        {
            StartCoroutine(beignBlowUp());
        }

        if (locActive == true)
        {
            SetDestination();
        }

        //Is idle
        if (navMeshAgent.speed <= 0)
        {
            animator.SetBool("IsMoving", false); ;
        }

        //Is running
        if (navMeshAgent.speed > 0)
        {
            animator.SetBool("IsMoving", true); ;
        }

        if (health.health <= 0){
            StartCoroutine(beignBlowUpNew());
        }
    }

    private void FixedUpdate()
    {

    }

    void SetDestination()
    {
        if (playerLoc.transform.position != null)
        {
            Vector3 targetVector = playerLoc.transform.position;
            navMeshAgent.SetDestination(targetVector);
        }
    }

    void detonation(){
        navMeshAgent.speed = 0;
        navMeshAgent.angularSpeed = 0;
        navMeshAgent.acceleration = 0;

        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

        foreach (Collider near in colliders)
        {
            EnemyHealth enemyHealth = near.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);
            }

            PlayerMovement playerMove = near.GetComponent<PlayerMovement>();
            if (playerMove != null){
                playerMove.TakeDamage(damage);
            }
        }
        navMeshAgent.SetDestination(transform.position);
        Instantiate(effect, transform.position, transform.rotation);
        if (deductExplode == true)
        {
            enemySpawn.deductEnemy(1);
        }
        Destroy(gameObject);
    }

    void detonationNew()
    {
        navMeshAgent.speed = 0;
        navMeshAgent.angularSpeed = 0;
        navMeshAgent.acceleration = 0;

        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

        foreach (Collider near in colliders)
        {
            EnemyHealth enemyHealth = near.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);
            }

            PlayerMovement playerMove = near.GetComponent<PlayerMovement>();
            if (playerMove != null)
            {
                playerMove.TakeDamage(damage);
            }
        }
        navMeshAgent.SetDestination(transform.position);
        Instantiate(effect, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    IEnumerator beignBlowUp(){
        yield return new WaitForSeconds(1f);
        detonation();
        health.collid.enabled = true;
        health.health = 0;
    }
    IEnumerator beignBlowUpNew()
    {
        yield return new WaitForSeconds(1f);
        detonationNew();
        health.collid.enabled = true;
        health.health = 0;
    }

}
