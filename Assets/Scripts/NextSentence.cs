using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextSentence : MonoBehaviour
{
    public GameObject pixie;

    public TextManager textMan;
    private PlayerMovement player;

    public bool hit;

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
        if (other.gameObject.tag == "Player" && hit == false){
            textMan.text.text = "";
            hit = true;
            //gameObject.SetActive(false);
            StartCoroutine(Next());
        }
    }

    IEnumerator Next(){
        yield return new WaitForSeconds(0.05f);
        textMan.NextSentence();
    }
}
