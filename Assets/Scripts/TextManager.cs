using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextManager : MonoBehaviour
{

    //code from https://www.youtube.com/watch?v=f-oSXg6_AMQ&t=336s

    public Text text;
    public string[] sentences;
    public int index;
    public float typingSpeed;
    public int sentenceNum;

    public Animator anim;

    private PlayerMovement player;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Type());
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (text.text == sentences[index]){
            player.cantMove = false;
        }
    }

    public IEnumerator Type()
    {
        foreach (char letter in sentences[index].ToCharArray())
        {
            text.text += letter;
            //anim.SetFloat("Blend", 1);
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    public void NextSentence()
    {
        if (index < sentences.Length - 1)
        {
            index++;
            text.text = "";
            sentenceNum++;
            StartCoroutine(Type());

        }
    }
}
