using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseButtons : MonoBehaviour
{
    public GameObject settingsMen;
    public GameObject resumeButt;
    public GameObject pauseButt;
    public GameObject settingsButt;
    public GameObject restartButt;

    public void getSettings(){
        settingsMen.SetActive(true);
        resumeButt.SetActive(false);
        pauseButt.SetActive(false);
        settingsButt.SetActive(false);
        restartButt.SetActive(false);
    }

    public void exitSettings(){
        settingsMen.SetActive(false);
        resumeButt.SetActive(true);
        pauseButt.SetActive(true);
        settingsButt.SetActive(true);
        restartButt.SetActive(true);
    }
}
