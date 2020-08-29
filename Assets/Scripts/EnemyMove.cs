using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMove : MonoBehaviour
{
    public float playerDist;

    private Transform playerLoc;

    NavMeshAgent navMeshAgent;

    public bool locActive;

    // Start is called before the first frame update
    void Start()
    {
        playerLoc = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        navMeshAgent = this.GetComponent<NavMeshAgent>();

    }

    private void Update()
    {
        if (Vector2.Distance(transform.position, playerLoc.transform.position) < playerDist){
            locActive = true;
        }

        if (Vector2.Distance(transform.position, playerLoc.transform.position) > playerDist)
        {
            locActive = false;
        }
    }

    private void FixedUpdate()
    {
        if (locActive == true)
        {
            SetDestination();
        }
    }

    void SetDestination(){
        if (playerLoc.transform.position != null){
            Vector3 targetVector = playerLoc.transform.position;
            navMeshAgent.SetDestination(targetVector);
        }
    }
}
