using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone : MonoBehaviour
{
    public Rigidbody rb;

    private float latestDirectionChangeTime;
    private readonly float directionChangeTime = 0.5f;
    public float characterVelocity;
    private Vector3 movementDirection;
    private Vector3 movementPerSecond;
    private Transform playerLoc;
    public bool farAway;

    public GameObject spawnEffect;

    public EnemyHealth health;
    public DroneShoot shoot;

    void Awake()
    {
        Instantiate(spawnEffect, transform.position, transform.rotation);
    }

    // Start is called before the first frame update
    void Start()
    {
        //latestDirectionChangeTime = 0f;
        playerLoc = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, playerLoc.transform.position) > 5 && health.health > 0)
        {
            transform.position = Vector3.MoveTowards(transform.position, playerLoc.position, characterVelocity * Time.deltaTime);
        }

        if (Vector2.Distance(transform.position, playerLoc.transform.position) < 5 && health.health > 0)
        {
            if (Time.time - latestDirectionChangeTime > directionChangeTime)
            {
                latestDirectionChangeTime = Time.time;
                calcuateNewMovementVector();
            }
            transform.position = new Vector3(transform.position.x + (movementPerSecond.x * Time.deltaTime),
        transform.position.y + (movementPerSecond.y * Time.deltaTime), transform.position.z + (movementPerSecond.z * Time.deltaTime));
        }

        if (Vector2.Distance(transform.position, playerLoc.transform.position) > 50 && health.health > 0)
        {
            transform.position = Vector3.MoveTowards(transform.position, playerLoc.position, characterVelocity * Time.deltaTime);
        }

        if (health.health <= 0){
            shoot.enabled = false;
            characterVelocity = 0;
            rb.constraints = RigidbodyConstraints.FreezeAll;
        }
    }

    void calcuateNewMovementVector()
    {
        //create a random direction vector with the magnitude of 1, later multiply it with the velocity of the enemy
        movementDirection = new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f)).normalized;
        movementPerSecond = movementDirection * characterVelocity;
    }

    private void OnCollisionEnter(Collision collision)
    {
        latestDirectionChangeTime = Time.time;
        calcuateNewMovementVector();
    }
}

