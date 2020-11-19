using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

    public class MainMenuStuff : MonoBehaviour
{
    public TMP_Text highScoreText;
    public TMP_Text highWave;

    // Start is called before the first frame update
    void Start()
    {
        int highScore = PlayerPrefs.GetInt("high_score", 0);
        highScoreText.text = "Highest Score: " + highScore;

        int highestWave = PlayerPrefs.GetInt("high_wave", 0);
        highWave.text = "Highest Wave: " + highestWave;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
