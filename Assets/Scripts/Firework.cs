using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firework : MonoBehaviour
{
    public float radius = 10f;
    public float damage = 10f;
    public float speed = 7f;

    public GameObject effect;

    // Start is called before the first frame update
    void Start()
    {
        
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
            EnemyHealth enemyHealth = near.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);
            }
            PointGiver point = near.GetComponent<PointGiver>();
            if (point != null)
            {
                point.GivePoint(point.targetPoints);
                Debug.Log("Got some points");
            }
        }
        Instantiate(effect, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
