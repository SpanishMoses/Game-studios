using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    //grenade code from https://www.youtube.com/watch?v=sglRyWQh79g&ab_channel=FPSBuilders

    public float delay = 3f;
    public float radius = 10f;
    public float damage = 10f;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("Explode", delay);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Explode(){
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

        foreach(Collider near in colliders){
            EnemyHealth enemyHealth = near.GetComponent<EnemyHealth>();
            if (enemyHealth != null){
                enemyHealth.TakeDamage(damage);
            }
        }

        Destroy(gameObject);
    }
}
