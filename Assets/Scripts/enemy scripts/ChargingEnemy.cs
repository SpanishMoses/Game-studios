using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChargingEnemy : MonoBehaviour
{
    private Vector3 target;
    private Vector3 normalizeDirection;

    public float playerDist;
    public float speed;

    private Transform playerLoc;

    public float time;
    public float timeBetweenCharges;

    public float chargeTime;
    public float MaxTime;

    public bool locActive;
    public bool isCharging;

    private Animator animator;

    public Rigidbody rb;

    public GameObject spawnEffect;

    void Awake()
    {
        Instantiate(spawnEffect, transform.position, transform.rotation);
    }

    // Start is called before the first frame update
    void Start()
    {
        playerLoc = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        isCharging = false;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, playerLoc.transform.position) < playerDist && isCharging == false)
        {
            locActive = true;
        }

        if (locActive == true && isCharging == false)
        {
            animator.SetBool("IsMoving", false); ;
            time += Time.deltaTime;
            rb.constraints = RigidbodyConstraints.FreezeAll;
            if (time >= timeBetweenCharges)
            {
                charge();
                time = 0;
                locActive = false;
                speed = 20;
                isCharging = true;
                rb.constraints = RigidbodyConstraints.None;
                rb.constraints = RigidbodyConstraints.FreezePositionY;
            }
        }

        if (isCharging == true){
            animator.SetBool("IsMoving", true);
            transform.position += normalizeDirection * speed * Time.deltaTime;
            chargeTime += Time.deltaTime;
            if (chargeTime >= MaxTime){
                isCharging = false;
                speed = 0;
                chargeTime = 0;
                Debug.Log("made it");
                locActive = true;
            }
        }
    }

    void charge(){
        target = new Vector3(playerLoc.position.x, transform.position.y, playerLoc.position.z);
        normalizeDirection = (target - transform.position).normalized;
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        /*if (collision.gameObject.tag == "Wall")
        {*/
            /*isCharging = false;
            speed = 0;
            chargeTime = 0;
            Debug.Log("made it");
            locActive = true;
        /*}
        */
        if (collision.gameObject.tag == "Player" && isCharging == true)
            {
                PlayerMovement playerMovement = collision.transform.GetComponent<PlayerMovement>();
                if (playerMovement != null)
                {
                    playerMovement.TakeDamage(1f);
                isCharging = false;
                speed = 0;
                chargeTime = 0;
                Debug.Log("made it");
                locActive = true;
                Vector3 direction = collision.transform.position - transform.position;
                direction.y = 0;
                playerMovement.AddImpact(direction, 50f);
            }
            
            } else{
            isCharging = false;
            speed = 0;
            chargeTime = 0;
            Debug.Log("made it");
            locActive = true;
        }
    }
}

