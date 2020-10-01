using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gnome : MonoBehaviour
{
    public Rigidbody rb;
    public EnemyHealth health;
    public bool alreadyDead;

    // Start is called before the first frame update
    void Start()
    {
        rb.AddForce(transform.up * 14f, ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        if (health.health <= 0){
            StartCoroutine(startNormalDeath());
            alreadyDead = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (alreadyDead == false){
            Destroy(gameObject);
        }
    }

    IEnumerator startNormalDeath()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
