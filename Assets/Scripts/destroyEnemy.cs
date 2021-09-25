using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyEnemy : MonoBehaviour
{
    

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Destroy(collision.gameObject);
        }
    }
}
