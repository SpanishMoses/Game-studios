using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UndergroundEnemy : MonoBehaviour
{
    public float playerDist;
    public float speed;

    //for spherecast
    public float radius;

    private Vector3 origin;
    private Vector3 direction;

    private Transform playerLoc;

    public NavMeshAgent navMeshAgent;

    public bool locActive;

    public Rigidbody rb;

    public GameObject gnome;

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
        }


        if (locActive == true)
        {
            SetDestination();
        }

        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

        foreach (Collider near in colliders)
        {
            PlayerMovement player = near.GetComponent<PlayerMovement>();
           if (player != null){
                gnome.SetActive(true);
                Debug.Log("i found you");
                locActive = false;
                navMeshAgent.speed = 0;
                StartCoroutine(beginFollow());
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

    IEnumerator beginFollow(){
        yield return new WaitForSeconds(5f);
        gnome.SetActive(false);
        locActive = true;
        navMeshAgent.speed = 3.5f;
    }
}
