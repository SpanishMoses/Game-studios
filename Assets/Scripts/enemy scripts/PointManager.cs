using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointManager : MonoBehaviour
{

    public int totalPoints;
    public Text pointText;

    // Start is called before the first frame update
    void Start()
    {
        totalPoints = PlayerPrefs.GetInt("Current_Score", 0);
        int currentPoints = PlayerPrefs.GetInt("high_score", 0);
        
    }

    // Update is called once per frame
    void Update()
    {
        PlayerPrefs.SetInt("Current_Score", totalPoints);
        pointText.text = "Score:" + totalPoints;

    }
}
