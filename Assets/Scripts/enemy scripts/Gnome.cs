using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gnome : MonoBehaviour
{
    public Rigidbody rb;
    public UndergroundEnemy underground;
    public EnemyHealth health;

    // Start is called before the first frame update
    void Start()
    {
        rb.AddForce(transform.up * 10f, ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        if (health.health <= 0){
            Destroy(gameObject);
            Destroy(underground);
        }
    }
}
