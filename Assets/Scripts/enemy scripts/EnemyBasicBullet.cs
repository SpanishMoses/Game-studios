using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBasicBullet : MonoBehaviour
{
    //https://www.youtube.com/watch?v=_Z1t7MNk0c4&t=682s
    //https://answers.unity.com/questions/853535/move-infinite-in-same-direction.html

    private Vector3 normalizeDirection;

    public float speed;

    public ParticleSystem impactEffect;

    private Transform player;
    private Vector3 target;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        target = new Vector3(player.position.x, player.position.y, player.position.z);
        normalizeDirection = (target - transform.position).normalized;

    }

    // Update is called once per frame
    void Update()
    {
        
        transform.position += normalizeDirection * speed * Time.deltaTime;

        /*if (transform.position.x == target.x && transform.position.y == target.y && transform.position.z == target.z)
            {
                
            }*/
    }

    void DestroyProjectile()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("yer");
        Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(gameObject);

        if (collision.gameObject.tag == "Player"){
            PlayerMovement playerMovement = collision.transform.GetComponent<PlayerMovement>();
            if (playerMovement != null){
                playerMovement.TakeDamage(4);
                playerMovement.ShakeIt();
            }
        }

    }
}
