using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneShoot : MonoBehaviour
{
    public GameObject spitEffect;
    public GameObject bullet;
    public GameObject parent;
    public float damage = 1f;

    public float shootTime;
    public float timeBetweenShots;

    public Animator animator;

    public EnemyHealth health;

    // Start is called before the first frame update
    void Start()
    {
        
        timeBetweenShots = Random.Range(2, 4);
    }

    // Update is called once per frame
    void Update()
    {
        
            shootTime += Time.deltaTime;
            if (shootTime >= timeBetweenShots)
            {
                shootTime = 0;
                timeBetweenShots = Random.Range(2, 4);
                shoot();
            }
        
            

    }

    private void FixedUpdate()
    {

    }

    private void shoot()
    {
        animator.SetTrigger("IsShooting");
        Instantiate(spitEffect, parent.transform.position, Quaternion.identity);
        Instantiate(bullet, parent.transform.position, Quaternion.identity);
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            //Destroy(gameObject);
            Debug.Log("YETT");
            /*PlayerMovement playHealth = other.transform.GetComponent<PlayerMovement>();
            if (playHealth != null){
                playHealth.TakeDamage(damage);
            }*/
        }
    }
}
