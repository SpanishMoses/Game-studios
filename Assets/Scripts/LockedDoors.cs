using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedDoors : MonoBehaviour
{
    public GameObject door;
    public int keysRequired;

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
            PlayerMovement play = collision.transform.GetComponent<PlayerMovement>();
            if (play.KeyAmount == keysRequired){
                door.SetActive(false);
                Debug.Log("collided");
            }
        }
    }
}
