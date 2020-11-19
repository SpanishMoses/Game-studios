using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PointManager : MonoBehaviour
{
    private int high;
    public int totalPoints;
    public TMP_Text pointText;

    // Start is called before the first frame update
    void Start()
    {
        totalPoints = PlayerPrefs.GetInt("Current_Score", 0);
        high = PlayerPrefs.GetInt("high_score", 0);
        
    }

    // Update is called once per frame
    void Update()
    {
        PlayerPrefs.SetInt("Current_Score", totalPoints);
        pointText.text = "Score:" + totalPoints;

        if (totalPoints > high){
            high = totalPoints;
            PlayerPrefs.SetInt("high_score", high);
        }

    }
}
