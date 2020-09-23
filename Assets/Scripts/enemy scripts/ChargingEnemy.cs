using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChargingEnemy : MonoBehaviour
{
    private Vector3 target;
    private Vector3 normalizeDirection;

    public float playerDist;
    public float backUpDist;
    public float speed;

    private Transform playerLoc;

    NavMeshAgent navMeshAgent;

    public bool locActive;
    public bool backUp;
    public bool isCharging;

    // Start is called before the first frame update
    void Start()
    {
        playerLoc = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        navMeshAgent = this.GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, playerLoc.transform.position) < playerDist && Vector2.Distance(transform.position, playerLoc.transform.position) > backUpDist && isCharging == false)
        {
            locActive = true;
            backUp = false;
        }
        if (Vector2.Distance(transform.position, playerLoc.transform.position) < backUpDist && isCharging == false)
        {
            backUp = true;
            locActive = false;
        }

        if (backUp == false)
        {
            locActive = true;
        }

        if (locActive == true && backUp == false && isCharging == false)
        {
            navMeshAgent = this.GetComponent<NavMeshAgent>();
            SetDestination();
        }
        if (backUp == true && locActive == false && isCharging == false)
        {
            AwayDestination();
        }

        if (isCharging == true){
            navMeshAgent.enabled = false;
            speed = 10;
            
            transform.position += normalizeDirection* speed * Time.deltaTime;
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

    void AwayDestination()
    {
        float distance = Vector3.Distance(transform.position, playerLoc.transform.position);

        if (distance <= backUpDist)
        {
            Vector3 dirToPlayer = transform.position - playerLoc.transform.position;
            Vector3 newPos = transform.position + dirToPlayer * 2;
            navMeshAgent.SetDestination(newPos);
            StartCoroutine(charge());
        }
    }

    IEnumerator charge(){
        target = new Vector3(playerLoc.position.x, playerLoc.position.y, playerLoc.position.z);
        normalizeDirection = (target - transform.position).normalized;
        yield return new WaitForSeconds(2f);
        isCharging = true;
        yield return new WaitForSeconds(2f);
        isCharging = false;
        navMeshAgent.enabled = true;
    }
}

