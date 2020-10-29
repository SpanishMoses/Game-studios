using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    public GameObject spitEffect;
    public GameObject bullet;
    EnemyMove mov;
    public GameObject parent;
    public float damage = 1f;

    public float shootTime;
    public float timeBetweenShots;

    public Animator animator;

    public EnemyHealth health;

    public bool startShootAnyways;

    public bool alreadyShoot;

    // Start is called before the first frame update
    void Start()
    {
        if (startShootAnyways == false)
        {
            mov = GetComponent<EnemyMove>();
        }
        timeBetweenShots = Random.Range(2, 4);
    }

    // Update is called once per frame
    void Update()
    {
        if (mov.locActive == true || mov.backUp == true || startShootAnyways == true)
        {
            shootTime += Time.deltaTime;
            if (shootTime >= timeBetweenShots){
                shootTime = 0;
                timeBetweenShots = Random.Range(2, 4);
                shoot();
                StartCoroutine(shootStop());
            }
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

    IEnumerator shootStop(){
        alreadyShoot = true;
        yield return new WaitForSeconds(0.5f);
        alreadyShoot = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player"){
            //Destroy(gameObject);
            Debug.Log("YETT");
            /*PlayerMovement playHealth = other.transform.GetComponent<PlayerMovement>();
            if (playHealth != null){
                playHealth.TakeDamage(damage);
            }*/
        }
    }
}
