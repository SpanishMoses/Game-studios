using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Consumables : MonoBehaviour
{

    public int amount;

    public bool isAmmo;
    public bool isHealth;
    public bool isKey;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player"){
            Debug.Log("yes");
            /*if (isHealth == true){
                PlayerMovement playHealth = collision.transform.GetComponent<PlayerMovement>();
                if (playHealth != null){
                    playHealth.health += amount;
                    Destroy(gameObject);
                }
            }

            if (isAmmo == true){

            }*/
        }
    }
}
