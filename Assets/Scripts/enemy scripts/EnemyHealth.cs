using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyHealth : MonoBehaviour
{
    //hit scan help from https://www.youtube.com/watch?v=THnivyG0Mvo&ab_channel=Brackeys

    public bool partOfArray;
    public bool specialDrop;

    public GameObject weapon;
    public GameObject endPortal;
    public GameObject bloodSplat;


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

    private void Update()
    {
        if (health <= minHealth){
            health = minHealth;
        }
        
        if (health <= 0f){
            for (int i = 0; i < parts.Length; i++){
                parts[i].SetActive(false);
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
            RaycastHit ray;
            if (Physics.Raycast(transform.position, -transform.up, out ray))
            {
                if (ray.collider != null)
                {
                    Instantiate(bloodSplat, ray.point + new Vector3(0, 0.2f, 0), Quaternion.LookRotation(-ray.normal));
                }
            }
        }
        if (partOfArray == true)
        {
            enemySpawn.deductEnemy(amountTaken);
            Destroy(gameObject);
            RaycastHit ray;
            if (Physics.Raycast(transform.position, -transform.up, out ray))
            {
                if (ray.collider != null)
                {
                    Instantiate(bloodSplat, ray.point + new Vector3(0, 0.2f, 0), Quaternion.LookRotation(-ray.normal));
                }
            }
        }
        if (specialDrop == true)
        {
            enemySpawn.deductEnemy(amountTaken);
            Instantiate(weapon, transform.position, Quaternion.identity);
            Destroy(gameObject);
            RaycastHit ray;
            if (Physics.Raycast(transform.position, -transform.up, out ray))
            {
                if (ray.collider != null)
                {
                    Instantiate(bloodSplat, ray.point + new Vector3(0, 0.2f, 0), Quaternion.LookRotation(-ray.normal));
                }
            }
        }
    }

    public void EndGame(){
        Destroy(gameObject);
        endPortal.SetActive(true);
            
        
    }
}
