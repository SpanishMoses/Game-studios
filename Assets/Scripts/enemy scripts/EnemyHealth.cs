using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyHealth : MonoBehaviour
{
    //hit scan help from https://www.youtube.com/watch?v=THnivyG0Mvo&ab_channel=Brackeys

    public bool partOfArray;
    public bool specialDrop;
    public bool splatBlood;

    public GameObject weapon;
    public GameObject endPortal;
    public GameObject bloodSplat;
    public GameObject soul;
    public GameObject deathPart;

    public GameObject[] parts;

    public float health;
    public float minHealth;

    public int amountTaken = 1;

    public GameObject parent;

    public Collider collid;

    public Animator animator;

    //public ArrayedEnemy array;
    public EnemySpawner enemySpawn;
    public EnemyMove enemyMove;
    public EnemyShoot enemyShoot;

    public AudioClip deathNoise;

    public AudioSource death;

    public bool isBear;
    public bool isUnicorn;
    public bool isDrone;
    public bool isExplode;
    public bool isPanda;

    public Achievment achieve;

    private void Start()
    {
        achieve = GameObject.FindGameObjectWithTag("Steam").GetComponent<Achievment>();
    }

    private void Update()
    {
        if (health <= minHealth){
            health = minHealth;
        }
        
        if (health <= 0f){
            for (int i = 0; i < parts.Length; i++){
                parts[i].SetActive(false);
                collid.enabled = false;
            }
        }

    }

    

    /*IEnumerator startDeath(){
        collid.enabled = false;
        //animator.SetTrigger("IsDead");
        yield return new WaitForSeconds(1f);
        enemySpawn.deductEnemy(amountTaken);
        Destroy(gameObject);
    }*/

    /*IEnumerator startNormalDeath(){
        collid.enabled = false;
        //animator.SetTrigger("IsDead");
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }*/

    public void StartDeath(){
        if (partOfArray == false){
            Destroy(gameObject);
        }
        if (partOfArray == true){
            enemySpawn.deductEnemy(amountTaken);
            Destroy(gameObject);
        }
        if (specialDrop == true){
            enemySpawn.deductEnemy(amountTaken);
            Instantiate(weapon, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    public void StartWispDeath()
    {
        if (partOfArray == false)
        {
            Destroy(gameObject);
            Instantiate(soul, transform.position, Quaternion.identity);
            Instantiate(deathPart, transform.position, Quaternion.identity);
            if (splatBlood == true)
            {
                RaycastHit ray;
                if (Physics.Raycast(transform.position, -transform.up, out ray))
                {
                    if (ray.collider != null)
                    {
                        Instantiate(bloodSplat, ray.point + new Vector3(0, 0.2f, 0), Quaternion.LookRotation(-ray.normal));
                    }
                }
            }
            if (isBear == true){
                achieve.bearKills++;
            }
            if (isUnicorn == true){
                achieve.unicornKills++;
            }
            if (isDrone == true){
                achieve.droneKills++;
            }
            if (isExplode == true){
                achieve.explodeKills++;
            }
            if (isPanda == true){
                achieve.pandaKills++;
            }
        }
        if (partOfArray == true)
        {
            enemySpawn.deductEnemy(amountTaken);
            Instantiate(soul, transform.position, Quaternion.identity);
            Instantiate(deathPart, transform.position, Quaternion.identity);
            Destroy(gameObject);
            if (splatBlood == true)
            {
                RaycastHit ray;
                if (Physics.Raycast(transform.position, -transform.up, out ray))
                {
                    if (ray.collider != null)
                    {
                        Instantiate(bloodSplat, ray.point + new Vector3(0, 0.2f, 0), Quaternion.LookRotation(-ray.normal));
                    }
                }
            }
            if (isBear == true)
            {
                achieve.bearKills++;
            }
            if (isUnicorn == true)
            {
                achieve.unicornKills++;
            }
            if (isDrone == true)
            {
                achieve.droneKills++;
            }
            if (isExplode == true)
            {
                achieve.explodeKills++;
            }
            if (isPanda == true)
            {
                achieve.pandaKills++;
            }
        }
        if (specialDrop == true)
        {
            enemySpawn.deductEnemy(amountTaken);
            Instantiate(weapon, transform.position, Quaternion.identity);
            Instantiate(soul, transform.position, Quaternion.identity);
            Instantiate(deathPart, transform.position, Quaternion.identity);
            Destroy(gameObject);
            if (splatBlood == true)
            {
                RaycastHit ray;
                if (Physics.Raycast(transform.position, -transform.up, out ray))
                {
                    if (ray.collider != null)
                    {
                        Instantiate(bloodSplat, ray.point + new Vector3(0, 0.05f, 0), Quaternion.LookRotation(-ray.normal));
                    }
                }
            }
            if (isBear == true)
            {
                achieve.bearKills++;
            }
            if (isUnicorn == true)
            {
                achieve.unicornKills++;
            }
            if (isDrone == true)
            {
                achieve.droneKills++;
            }
            if (isExplode == true)
            {
                achieve.explodeKills++;
            }
            if (isPanda == true)
            {
                achieve.pandaKills++;
            }
        }
    }

    public void EndGame(){
        Destroy(gameObject);
        endPortal.SetActive(true);
            
        
    }
}
