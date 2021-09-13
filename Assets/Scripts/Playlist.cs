using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Playlist : MonoBehaviour
{
    public int songNum;

    public TextMeshProUGUI text;

    // Start is called before the first frame update
    void Start()
    {
        songNum = PlayerPrefs.GetInt("Song", 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (songNum == 1){
            text.text = "Slaughter";
            PlayerPrefs.SetInt("Song", 1);
        }
        if (songNum == 2){
            text.text = "Unicorn Abs";
            PlayerPrefs.SetInt("Song", 2);
        }
        if (songNum == 3){
            text.text = "Bubble Faerie";
            PlayerPrefs.SetInt("Song", 3);
        }
        if (songNum == 4){
            text.text = "Fight Back";
            PlayerPrefs.SetInt("Song", 4);
        }

        if (songNum == 5)
        {
            text.text = "Shuffle";
            PlayerPrefs.SetInt("Song", 5);
        }

        if (songNum < 1){
            songNum = 5;
        }

        if (songNum > 5){
            songNum = 1;
        }
    }

    public void increase(){
        songNum += 1;
    }

    public void decrease(){
        songNum -= 1;
    }
}
