using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public void SinglePlayerbutton(string newGameLevel)
    {
        Time.timeScale = 1f;
        StartCoroutine(loadlevel());
        IEnumerator loadlevel()
        {
            yield return new WaitForSeconds(1);
            SceneManager.LoadScene(newGameLevel);
        }
    }

    public void resetScore(){
        PlayerPrefs.SetInt("Current_Score", 0);
    }
}
