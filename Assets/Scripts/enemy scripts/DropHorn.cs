using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropHorn : MonoBehaviour
{
    public Transform unicorn;
    public GameObject knife;
    public EnemyHealth health;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (health.health <= 0){
            Instantiate(knife, unicorn.position, Quaternion.identity);
        }
    }
}
