using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoading : MonoBehaviour
{
    //code from https://www.youtube.com/watch?v=fxxoACKCWVo&ab_channel=GameDevHQ

    public Image progressBar;
    public string newLevel;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadAsyncOperation(newLevel));
    }

    IEnumerator LoadAsyncOperation(string newGameLevel)
    {
        AsyncOperation gameLevel = SceneManager.LoadSceneAsync(newGameLevel);

        while (gameLevel.progress < 1){
            progressBar.fillAmount = gameLevel.progress;
            yield return new WaitForEndOfFrame();
        }
        
    }
}
