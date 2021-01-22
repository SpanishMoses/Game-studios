using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MakingSure : MonoBehaviour
{

    public GameObject resumeButt;
    public GameObject restartButt;
    public GameObject settingsButt;
    public GameObject exitButt;

    public GameObject yesButt;
    public GameObject yesButt2;
    public GameObject noButt;
    public GameObject confirmationText;

    public void confirmation(){
        //StartCoroutine(yes());
        resumeButt.SetActive(false);
        restartButt.SetActive(false);
        exitButt.SetActive(false);
        settingsButt.SetActive(false);

        yesButt.SetActive(true);
        noButt.SetActive(true);
        confirmationText.SetActive(true);
    }

    public void regret(){
        //StartCoroutine(no());
        resumeButt.SetActive(true);
        restartButt.SetActive(true);
        exitButt.SetActive(true);
        settingsButt.SetActive(true);

        yesButt.SetActive(false);
        yesButt2.SetActive(false);
        noButt.SetActive(false);
        confirmationText.SetActive(false);
    }

    IEnumerator yes(){
        yield return new WaitForSeconds(0.25f);
        
    }

    IEnumerator no(){
        yield return new WaitForSeconds(0.25f);
        
    }

}
