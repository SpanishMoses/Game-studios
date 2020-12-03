using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lock : MonoBehaviour
{
    public AudioSource good;
    public bool unlocked;
    public int keysRequired;
    public SpriteRenderer rend;

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
        if (collision.gameObject.tag == "Player")
        {
            PlayerMovement play = collision.transform.GetComponent<PlayerMovement>();
            if (play.KeyAmount <= 1 && unlocked == false)
            {
                play.KeyAmount -= 1;
                unlocked = true;
                rend.color = Color.black;
                good.Play();
            }
        }
    }
}
