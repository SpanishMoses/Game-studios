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

    public bool locActive;
    public bool isCharging;

    // Start is called before the first frame update
    void Start()
    {
        playerLoc = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        isCharging = false;
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
            time += Time.deltaTime;
            if (time >= timeBetweenCharges)
            {
                charge();
                time = 0;
                locActive = false;
                speed = 20;
                
                
            }
        }

        if (isCharging == true){
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
            if (transform.position.x == target.x && transform.position.y == target.y && transform.position.z == target.z)
            {
                isCharging = false;
                speed = 0;
                Debug.Log("made it");
                locActive = true;
            }
        }
    }

    void charge(){
        target = new Vector3(playerLoc.position.x, playerLoc.position.y, playerLoc.position.z);
        isCharging = true;


    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player" && isCharging == true)
            {
                PlayerMovement playerMovement = collision.transform.GetComponent<PlayerMovement>();
                if (playerMovement != null)
                {
                    playerMovement.TakeDamage(1f);
                }
            }
    }
}

