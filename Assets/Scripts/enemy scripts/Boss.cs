using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    public GameObject rumbling;
    public GameObject[] drones;
    public GameObject spawnPoint;
    public GameObject noiseBlock;

    public BoxCollider collid;
    public EnemyMove mov;
    public EnemyHealth health;

    public GameObject spitEffect;
    public GameObject bullet;
    public GameObject parent;

    public bool canShoot;
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

    public float radius;
    public bool expandDong;

    public GameObject healthSlide;
    public Slider healthSlideReal;

    public AudioSource bossNoise;
    public AudioClip spawnSound;
    public AudioClip shootSound;
    public AudioClip staggeredSound;
    public AudioClip deathSound;



    private void Awake()
    {
        healthSlide.SetActive(false);
        
    }

    // Start is called before the first frame update
    void Start()
    {
        canRumble = true;
        canShoot = true;
        collid.enabled = false;
        for (int i = 0; i < drones.Length; i++)
        {
            dronesStaggared = drones.Length;
        }
        timeBetweenShots = Random.Range(2, 4);
        playerLoc = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        navMeshAgent = this.GetComponent<NavMeshAgent>();
        bossNoise.clip = spawnSound;
        bossNoise.Play();
        healthSlide = GameObject.FindGameObjectWithTag("BossHealth");
        healthSlideReal = healthSlide.GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {

        healthSlideReal.maxValue = 700;
        healthSlideReal.value = health.health;

        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

            foreach (Collider near in colliders)
            {

                PlayerMovement playerMove = near.GetComponent<PlayerMovement>();
                if (playerMove != null)
                {
                    playerMove.TakeDamage(10);
                    playerMove.ShakeIt();
                    playerMove.mouse.camAnim.SetTrigger("camShake2");
                    Vector3 direction = playerMove.transform.position - transform.position;
                    playerMove.AddImpact(direction, 300f);
                }
            }
        

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
                noiseBlock.SetActive(true);
                //mov.locActive = false;
                canRumble = false;
                canShoot = false;
                rumbleTime = 0;
                shootTime = 0;
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
            animator.SetBool("IsHurt", true);
            staggerTime += Time.deltaTime;
            if (staggerTime >= maxStaggerTime){
                staggered = false;
                staggerTime = 0;
                //mov.locActive = true;
                collid.enabled = false;
                animator.SetBool("IsHurt", false);
                SetDestination();
                resetDrones = true;
                canShoot = true;
                canRumble = true;
                noiseBlock.SetActive(false);
                dronesStaggared = drones.Length;
            }
        }

        if (resetDrones == true){
            StartCoroutine(reset());
        }

        if (expandDong == true){
            

            
        }

        if (canShoot == true){ 
        shootTime += Time.deltaTime;
            if (shootTime >= timeBetweenShots)
            {
                shootTime = 0;
                timeBetweenShots = Random.Range(2, 4);
                shoot();
                StartCoroutine(shootStop());
            }
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
        bossNoise.clip = shootSound;
        bossNoise.Play();
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

    IEnumerator shove()
    {
        expandDong = true;
        yield return new WaitForSeconds(0.5f);
        expandDong = false;
    }

    void deadSound(){
        bossNoise.clip = deathSound;
        bossNoise.Play();
        healthSlide.SetActive(false);
    }
}
