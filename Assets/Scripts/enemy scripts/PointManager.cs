using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PointManager : MonoBehaviour
{
    //shake code from https://www.youtube.com/watch?v=kzHHAdvVkto

    private int high;
    public int totalPoints;
    public TMP_Text pointText;
    Vector3 ghostInitialPosition;
    float shakeTime = 0.05f;
    float shakeAmount = 5f;
    public GameObject ghost;

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

    public void ShakeIt()
    {
        ghostInitialPosition = ghost.transform.position;
        InvokeRepeating("StartGhostShaking", 0f, 0.005f);
        Invoke("StopGhostShaking", shakeTime);
    }

    void StartGhostShaking()
    {
        float ghostShakingOffsetX = Random.value * shakeAmount * 2 - shakeAmount;
        float ghostShakingOffsetY = Random.value * shakeAmount * 2 - shakeAmount;
        Vector3 ghostIntermadiatePosition = ghost.transform.position;
        ghostIntermadiatePosition.x += ghostShakingOffsetX;
        ghostIntermadiatePosition.y += ghostShakingOffsetY;
        ghost.transform.position = ghostIntermadiatePosition;
    }

    void StopGhostShaking()
    {
        CancelInvoke("StartGhostShaking");
        ghost.transform.position = ghostInitialPosition;
    }

}
