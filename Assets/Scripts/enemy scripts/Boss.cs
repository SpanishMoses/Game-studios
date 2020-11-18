using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Boss : MonoBehaviour
{
    public GameObject rumbling;
    public GameObject[] drones;
    public GameObject spawnPoint;

    public BoxCollider collid;
    public EnemyMove mov;

    public GameObject spitEffect;
    public GameObject bullet;
    public GameObject parent;

    public bool alreadyShoot;

    public bool staggered;
    public float staggerTime;
    public float maxStaggerTime;

    public bool canRumble;
    public float rumbleTime;
    public float maxRumbleTime;

    public int dronesStaggared;

    public float shootTime;
    public float timeBetweenShots;

    public Animator animator;

    public Transform playerLoc;
    public NavMeshAgent navMeshAgent;

    public bool resetDrones;
    public bool enableReset;

    // Start is called before the first frame update
    void Start()
    {
        canRumble = true;
        collid.enabled = false;
        for (int i = 0; i < drones.Length; i++)
        {
            dronesStaggared = drones.Length;
        }
        timeBetweenShots = Random.Range(2, 4);
        playerLoc = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        navMeshAgent = this.GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {

        SetDestination();

        for (int i = 0; i < drones.Length; i++){
            /*if (drones[0].GetComponent<BossDrone>().stopped == true && drones[1].GetComponent<BossDrone>().stopped == true)
            {
                collid.enabled = true;               
                mov.locActive = false;
                staggered = true;
            }
        }*/
            if (dronesStaggared == 0){
                staggered = true;
                collid.enabled = true;
                //mov.locActive = false;
                canRumble = false;
                rumbleTime = 0;
                //StartCoroutine(resetDrones());
            }
        }

        /*if (dronesStaggared > 0){
            SetDestination();
        }*/

        if (canRumble == true){
            rumbleTime += Time.deltaTime;
            if (rumbleTime >= maxRumbleTime){
                sendWave();
                rumbleTime = 0;
            }
        }

        if (staggered == true){
            navMeshAgent.SetDestination(transform.position);
            staggerTime += Time.deltaTime;
            if (staggerTime >= maxStaggerTime){
                staggered = false;
                staggerTime = 0;
                //mov.locActive = true;
                collid.enabled = false;
                SetDestination();
                resetDrones = true;
                dronesStaggared = drones.Length;
            }
        }

        if (resetDrones == true){
            StartCoroutine(reset());
        }

        shootTime += Time.deltaTime;
        if (shootTime >= timeBetweenShots)
        {
            shootTime = 0;
            timeBetweenShots = Random.Range(2, 4);
            shoot();
            StartCoroutine(shootStop());
        }
    }

    void SetDestination()
    {
        if (playerLoc.transform.position != null)
        {
            Vector3 targetVector = playerLoc.transform.position;
            navMeshAgent.SetDestination(targetVector);
        }
    }

        private void shoot()
    {
        animator.SetTrigger("IsShooting");
        Instantiate(spitEffect, parent.transform.position, Quaternion.identity);
        Instantiate(bullet, parent.transform.position, Quaternion.identity);
    }

    IEnumerator shootStop()
    {
        alreadyShoot = true;
        yield return new WaitForSeconds(1);
        alreadyShoot = false;
    }

    public void Add(int num){
        dronesStaggared += num;
    }

    public void Sub(int num){
        dronesStaggared -= num;
    }

    void sendWave()
    {
        //Vector3 direction = mov.playerLoc.position - transform.position;
        GameObject grenadeInstance = Instantiate(rumbling, spawnPoint.transform.position, Quaternion.identity);
    }

    IEnumerator reset(){
        yield return new WaitForSeconds(0.01f);
        resetDrones = false;
    }

    
}
