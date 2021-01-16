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
    public float positionX;
    public float positionY;
    public float positionZ;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadAsyncOperation(newLevel));
        PlayerPrefs.SetFloat("CheckPointX", positionX);
        PlayerPrefs.SetFloat("CheckPointY", positionY);
        PlayerPrefs.SetFloat("CheckPointZ", positionZ);
        PlayerPrefs.SetInt("NOHIT", 0);
    }

    IEnumerator LoadAsyncOperation(string newGameLevel)
    {

        yield return new WaitForSeconds(5f);
        AsyncOperation gameLevel = SceneManager.LoadSceneAsync(newGameLevel);

        /*while (gameLevel.progress < 1){
            progressBar.fillAmount = gameLevel.progress;
            yield return new WaitForEndOfFrame();
        }*/
        
    }
}
