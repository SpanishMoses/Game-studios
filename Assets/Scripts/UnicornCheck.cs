using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnicornCheck : MonoBehaviour
{

    public MouseLook mouse;
    public Animator basic;
    public Animator special;
    public SpriteRenderer specialSprite;
    public SpriteRenderer basicSprite;

    // Start is called before the first frame update
    void Start()
    {
        mouse = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<MouseLook>();
    }

    // Update is called once per frame
    void Update()
    {
        if (mouse.enableSpecial == true){
            special.enabled = true;
            specialSprite.enabled = true;
            basic.enabled = false;
            basicSprite.enabled = false;
        } else{
            special.enabled = false;
            specialSprite.enabled = false;
            basic.enabled = true;
            basicSprite.enabled = true;
        }
    }
}
