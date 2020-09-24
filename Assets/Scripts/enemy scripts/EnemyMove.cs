using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMove : MonoBehaviour
{
    //help from https://www.youtube.com/watch?v=NGGoOa4BpmY
    //backing up from https://www.youtube.com/watch?v=Zjlg9F3FRJs&ab_channel=Jayanam

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
        if (Vector2.Distance(transform.position, playerLoc.transform.position) < playerDist)
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

        if (locActive == true && backUp == false)
        {
            SetDestination();
        }
        if (backUp == true && locActive == false)
        {
            AwayDestination();
        }
    }

    private void FixedUpdate()
    {
        
    }

    void SetDestination(){
        if (playerLoc.transform.position != null){
            Vector3 targetVector = playerLoc.transform.position;
            navMeshAgent.SetDestination(targetVector);
        }
    }

    void AwayDestination()
    {
        /*if (playerLoc.transform.position != null)
        {
            Vector3 targetVector = playerLoc.transform.position;
            navMeshAgent.SetDestination(targetVector);
        }*/

        float distance = Vector3.Distance(transform.position, playerLoc.transform.position);

        if (distance < backUpDist){
            Vector3 dirToPlayer = transform.position - playerLoc.transform.position;
            Vector3 newPos = transform.position + dirToPlayer * 2;
            navMeshAgent.SetDestination(newPos);
        }
    }
}
