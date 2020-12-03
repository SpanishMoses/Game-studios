using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankEnemy : MonoBehaviour
{
    public EnemyMove enemy;
    public GameObject rumbling;
    public Transform spawnPoint;
    public Animator anim;

    public bool isDead;

    public AudioSource windUp;
    public AudioClip windUpNoise;
    public AudioClip groundPoundNoise;

    public EnemyHealth health;

    public float time;
    public float maxTime;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      if (health.health <= 0){
            isDead = true;
        }   

      if (isDead == false){
            time += Time.deltaTime;
            if (time >= maxTime){
                anim.SetTrigger("Shockwave");
                time = 0;
            }
        }

    }

    public void sendWave(){
        Vector3 direction = enemy.playerLoc.position - transform.position;
        GameObject grenadeInstance = Instantiate(rumbling, spawnPoint.transform.position, Quaternion.identity);
        //grenadeInstance.transform.forward = direction.normalized;
        //grenadeInstance.GetComponent<Rigidbody>().AddForce(direction.normalized * 25f, ForceMode.Impulse);
    }

    /*IEnumerator send(){
        yield return new WaitForSeconds(3);
        //sendWave();
        anim.SetTrigger("Shockwave");
        StartCoroutine(send());
    }*/

    public void wind(){
        windUp.clip = windUpNoise;
        windUp.Play();
    }

    public void pound(){
        windUp.clip = groundPoundNoise;
        windUp.Play();
    }
}
