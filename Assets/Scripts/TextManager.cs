using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextManager : MonoBehaviour
{

    //code from https://www.youtube.com/watch?v=f-oSXg6_AMQ&t=336s

    public Animator anim;
    public GameObject pixie;
    public GameObject textBox;
    public GameObject reticle;
    public float speed = 0.5f;

    public Text text;
    public string[] sentences;
    public int index;
    public float typingSpeed;
    public int sentenceNum;

    private PlayerMovement player;

    RectTransform rect;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Type());
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        anim = pixie.gameObject.GetComponent<Animator>();
        rect = pixie.gameObject.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (text.text == sentences[index] && sentenceNum < 9){
            player.cantMove = false;
            anim.SetFloat("Blend", 0);
            
        } else if (sentenceNum == 9){
            player.cantMove = true;
            if (text.text == sentences[index] && sentenceNum == 9 && Input.GetKey(KeyCode.Mouse0)){
                player.cantMove = false;
                anim.SetBool("MousClick", true);
                StartCoroutine(whipe());
                sentenceNum++;
            }

            if (sentenceNum == 9){
                anim.SetBool("TutDone", true);
                
            }
        }
    }

    IEnumerator whipe(){
        yield return new WaitForSeconds(1.5f);
        pixie.SetActive(false);
        textBox.SetActive(false);
    }

    public IEnumerator Type()
    {
        foreach (char letter in sentences[index].ToCharArray())
        {
            text.text += letter;
            anim.SetFloat("Blend", 1);
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
