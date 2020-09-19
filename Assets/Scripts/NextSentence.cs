using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextSentence : MonoBehaviour
{

    public TextManager textMan;
    private PlayerMovement player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player"){
            textMan.NextSentence();
            gameObject.SetActive(false);
            player.cantMove = true;
        }
    }
}
