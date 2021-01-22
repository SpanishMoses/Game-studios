using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firework : MonoBehaviour
{
    public float radius = 10f;
    public float damage = 10f;
    public float speed = 7f;

    public GameObject blood;
    public GameObject effect;
    public GameObject confetti;
    public GameObject candy;

    public MouseLook mouse;

    public Achievment achieve;

    // Start is called before the first frame update
    void Start()
    {
        achieve = GameObject.FindGameObjectWithTag("Steam").GetComponent<Achievment>();
        mouse = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<MouseLook>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnCollisionEnter(Collision collision){
        Explode();
    }

    void Explode(){
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

        foreach (Collider near in colliders)
        {
            
            PointGiver point = near.GetComponent<PointGiver>();
            if (point != null)
            {
                point.GivePoint(point.targetPoints);
                point.TakeDamage(damage);
                if (point.health.health <= 0)
                {
                    achieve.fireWorkKill++;
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
        if (mouse.enableCandy == true){
            Instantiate(candy, transform.position, transform.rotation);
        }
        if (mouse.enableCandy == false){
            Instantiate(effect, transform.position, transform.rotation);
        }
        
        Destroy(gameObject);
    }
}
