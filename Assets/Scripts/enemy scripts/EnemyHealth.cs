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
    }

    public void TakeDamage(float amount){
        health -= amount;
        if (health <= 0f && partOfArray == false)
        {
            Debug.Log("dead");
            //StartCoroutine(startNormalDeath());
            animator.SetTrigger("IsDead");
            enemyMove.navMeshAgent.speed = 0;
            enemyShoot.enabled = false;
            collid.enabled = false;
            death.clip = deathNoise;
            death.Play();
        }
            if (health <= 0 && partOfArray == true){
            //StartCoroutine(startDeath());
            animator.SetTrigger("IsDead");
            Debug.Log("started");
            enemyMove.navMeshAgent.speed = 0;
            enemyShoot.enabled = false;
            collid.enabled = false;
            death.clip = deathNoise;
            death.Play();
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
        }
        if (partOfArray == true)
        {
            enemySpawn.deductEnemy(amountTaken);
            Destroy(gameObject);
        }
        if (specialDrop == true)
        {
            enemySpawn.deductEnemy(amountTaken);
            Instantiate(weapon, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    public void EndGame(){
        Destroy(gameObject);
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("LevelThree"))
        {
            Cursor.lockState = CursorLockMode.None;
            SceneManager.LoadScene("main menu");
        }
    }
}
