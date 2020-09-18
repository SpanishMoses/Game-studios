using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMove : MonoBehaviour
{
    //help from https://www.youtube.com/watch?v=NGGoOa4BpmY

    public float playerDist;
    public float backUpDist;
    public float speed;

    private Transform playerLoc;

    NavMeshAgent navMeshAgent;

    public bool locActive;
    public bool backUp;

    // Start is called before the first frame update
    void Start()
    {
        playerLoc = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        navMeshAgent = this.GetComponent<NavMeshAgent>();

    }

    private void Update()
    {
        if (Vector2.Distance(transform.position, playerLoc.transform.position) < playerDist && Vector2.Distance(transform.position, playerLoc.transform.position) > backUpDist)
        {
            locActive = true;
            backUp = false;
        }

            if (Vector2.Distance(transform.position, playerLoc.transform.position) < backUpDist)
        {
            backUp = true;
            locActive = false;
        }

        if (backUp == false){
            locActive = true;
        }
    }

    private void FixedUpdate()
    {
        if (locActive == true && backUp == false)
        {
            SetDestination();
        }
        if (backUp == true && locActive == false){
            AwayDestination();
        }
    }

    void SetDestination(){
        if (playerLoc.transform.position != null){
            Vector3 targetVector = playerLoc.transform.position;
            navMeshAgent.SetDestination(targetVector);
        }
    }

    void AwayDestination()
    {
        if (playerLoc.transform.position != null)
        {
            Vector3 targetVector = playerLoc.transform.position;
            navMeshAgent.SetDestination(-targetVector);
        }
    }
}
