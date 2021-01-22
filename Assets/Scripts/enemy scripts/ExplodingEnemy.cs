using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Steamworks;

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
    public GameObject candy;

    public AudioSource fuse;

    public Achievment achieve;

    public MouseLook mouse;

    // Start is called before the first frame update
    void Start()
    {
        playerLoc = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        navMeshAgent = this.GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        achieve = GameObject.FindGameObjectWithTag("Steam").GetComponent<Achievment>();
        mouse = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<MouseLook>();
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
        }

        /*if (Vector2.Distance(transform.position, playerLoc.transform.position) < blowUpDist)
        {
            Debug.Log("ayyyy");
            StartCoroutine(beignBlowUp());
        }*/

        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

        foreach (Collider near in colliders)
        {
            
            PlayerMovement playerMove = near.GetComponent<PlayerMovement>();
            if (playerMove != null)
            {
                StartCoroutine(beignBlowUp());
            }
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
            PointGiver point = near.GetComponent<PointGiver>();
            if (point != null){
                point.TakeDamage(damage);
            }

            PlayerMovement playerMove = near.GetComponent<PlayerMovement>();
            if (playerMove != null){
                playerMove.TakeDamage(damage);
                playerMove.ShakeIt();
                playerMove.mouse.camAnim.SetTrigger("camShake2");
            }
        }
        navMeshAgent.SetDestination(transform.position);
        if (mouse.enableCandy == true)
        {
            Instantiate(candy, transform.position, transform.rotation);
        }
        if (mouse.enableCandy == false)
        {
            Instantiate(effect, transform.position, transform.rotation);
        }
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
            PointGiver point = near.GetComponent<PointGiver>();
            if (point != null)
            {
                point.TakeDamage(damage);
            }

            PlayerMovement playerMove = near.GetComponent<PlayerMovement>();
            if (playerMove != null)
            {
                playerMove.TakeDamage(damage);
                playerMove.ShakeIt();
                playerMove.mouse.camAnim.SetTrigger("camShake2");
            }
        }
        navMeshAgent.SetDestination(transform.position);
        if (mouse.enableCandy == true)
        {
            Instantiate(candy, transform.position, transform.rotation);
        }
        if (mouse.enableCandy == false)
        {
            Instantiate(effect, transform.position, transform.rotation);
        }
        Destroy(gameObject);
    }

    IEnumerator beignBlowUp(){
        yield return new WaitForSeconds(1f);
        achieve.explodeKills++;
        detonation();
        health.collid.enabled = true;
        health.health = 0;
    }
    IEnumerator beignBlowUpNew()
    {
        yield return new WaitForSeconds(1f);
        achieve.explodeKills++;
        detonationNew();
        health.collid.enabled = true;
        health.health = 0;
    }

    public void lightfuse(){
        fuse.Play();
    }

}
