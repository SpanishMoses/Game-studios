using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    //grenade code from https://www.youtube.com/watch?v=sglRyWQh79g&ab_channel=FPSBuilders

    public float delay = 3f;
    public float radius = 10f;
    public float damage = 10f;

    public GameObject blood;
    public GameObject confetti;
    public GameObject candy;

    public GameObject effect;

    public AudioSource hit;

    public MouseLook mouse;
    public PlayerMovement play;

    public Achievment achieve;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("Explode", delay);
        mouse = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<MouseLook>();
        achieve = GameObject.FindGameObjectWithTag("Steam").GetComponent<Achievment>();
        play = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        hit.Play();
    }

    void Explode(){
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

        foreach(Collider near in colliders){
            
            PointGiver point = near.GetComponent<PointGiver>();
            if (point != null)
            {
                point.GivePoint(point.targetPoints);
                if (play.canInstaKill == false)
                {
                    point.TakeDamage(damage);
                }
                if (play.canInstaKill == true)
                {
                    point.TakeDamage(1000);
                }
                if (point.health.health <= 0)
                {
                    achieve.grenadeKill++;
                }
                if (mouse.enableCon == false)
                {
                    Instantiate(blood, near.transform.position, Quaternion.identity);
                }
                if (mouse.enableCon == true)
                {
                    Instantiate(confetti, near.transform.position, Quaternion.identity);
                }

                Debug.Log("Got some points");
            }
            PlayerMovement playerMove = near.GetComponent<PlayerMovement>();
            if (playerMove != null)
            {
                playerMove.mouse.camAnim.SetTrigger("camShake2");
            }
            BossDrone bossDrone = near.transform.GetComponent<BossDrone>();
            if (bossDrone != null)
            {
                bossDrone.TakeDamage(damage);
                if (mouse.enableCon == false)
                {
                    Instantiate(blood, near.transform.position, Quaternion.identity);
                }
                if (mouse.enableCon == true)
                {
                    Instantiate(confetti, near.transform.position, Quaternion.identity);
                }
            }
        }

        Instantiate(effect, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
