using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedDoors : MonoBehaviour
{
    public GameObject door;
    public GameObject door1;
    public GameObject door2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (door1.GetComponent<Lock>().unlocked == true && door2.GetComponent<Lock>().unlocked == true){
            door.SetActive(false);
        }
    }

    /*private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player"){
            PlayerMovement play = collision.transform.GetComponent<PlayerMovement>();
            if (play.KeyAmount == keysRequired){
                door.SetActive(false);
                Debug.Log("collided");
            }
        }
    }*/
}
