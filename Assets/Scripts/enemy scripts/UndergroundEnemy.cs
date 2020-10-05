using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UndergroundEnemy : MonoBehaviour
{
    public float playerDist;
    public float speed;

    public float spawnTime;
    public float maxSpawnTime;

    //for spherecast
    public float radius;

    private Vector3 origin;
    private Vector3 direction;

    private Transform playerLoc;

    public bool locActive;

    public Rigidbody rb;

    public GameObject gnome;

    public Transform spawnPoint;

    public Gnome myGnome;

    // Start is called before the first frame update
    void Start()
    {
        playerLoc = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        locActive = true;
    }

    private void Update()
    {
        

        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

        foreach (Collider near in colliders)
        {
            PlayerMovement player = near.GetComponent<PlayerMovement>();
            if (player != null)
            {
                locActive = false;
            }
            else
                locActive = true;
    }

        if (locActive == false){
        spawnTime += Time.deltaTime;
                if (spawnTime >= maxSpawnTime){
                    Debug.Log("i found you");
                    spawnTime = 0;
                    locActive = false;
                spawnGnome();
            }
        }

        
    }

    void spawnGnome()
    {
        Instantiate(gnome, spawnPoint.position, Quaternion.identity);
    }



}
