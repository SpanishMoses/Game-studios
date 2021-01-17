using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DisplayAchieve : MonoBehaviour
{
    public TextMeshProUGUI text;
    public GameObject achievements;
    public GameObject menu;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void hoverNow(string desc)
    {
        text.text = desc;
    }

    public void display(){
        menu.SetActive(false);
        achievements.SetActive(true);
    }

    public void back(){
        achievements.SetActive(false);
        menu.SetActive(true);
    }
}
