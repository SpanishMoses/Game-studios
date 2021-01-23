using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PresentManager : MonoBehaviour
{

    public bool tutorialFound;
    public bool level1Found;
    public bool level2Found;
    public bool level3Found;
    public bool level4Found;

    public bool test1;

    public GameObject present;

    // Start is called before the first frame update
    void Start()
    {
        tutorialFound = PlayerPrefs.GetInt("TUTORIAL", 0) > 0;
        level1Found = PlayerPrefs.GetInt("LEVEL1", 0) > 0;
        level2Found = PlayerPrefs.GetInt("LEVEL2", 0) > 0;
        level3Found = PlayerPrefs.GetInt("LEVEL3", 0) > 0;
        level4Found = PlayerPrefs.GetInt("LEVEL4", 0) > 0;

        test1 = PlayerPrefs.GetInt("TEST", 0) > 0;
    }

    // Update is called once per frame
    void Update()
    {
        int yesTutorial;
        yesTutorial = tutorialFound ? 1 : 0;

        if (tutorialFound == true && SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Tutorial Level"))
        {
            PlayerPrefs.SetInt("TUTORIAL", 1);
            Destroy(present);         
        }
        else
        {
            PlayerPrefs.SetInt("TUTORIAL", 0);
        }

        int yesLevel1;
        yesLevel1 = level1Found ? 1 : 0;

        if (level1Found == true && SceneManager.GetActiveScene() == SceneManager.GetSceneByName("LevelOne"))
        {
            PlayerPrefs.SetInt("LEVEL1", 1);
            Destroy(present);
        }
        else
        {
            PlayerPrefs.SetInt("LEVEL1", 0);
        }

        int yesLevel2;
        yesLevel2 = level2Found ? 1 : 0;

        if (level2Found == true && SceneManager.GetActiveScene() == SceneManager.GetSceneByName("LevelTwo"))
        {
            PlayerPrefs.SetInt("LEVEL2", 1);
            Destroy(present);
        }
        else
        {
            PlayerPrefs.SetInt("LEVEL2", 0);
        }

        int yesLevel3;
        yesLevel3 = level3Found ? 1 : 0;

        if (level3Found == true && SceneManager.GetActiveScene() == SceneManager.GetSceneByName("LevelThree"))
        {
            PlayerPrefs.SetInt("LEVEL3", 1);
            Destroy(present);
        }
        else
        {
            PlayerPrefs.SetInt("LEVEL3", 0);
        }

        int yesLevel4;
        yesLevel4 = level4Found ? 1 : 0;

        if (level4Found == true && SceneManager.GetActiveScene() == SceneManager.GetSceneByName("LevelFour"))
        {
            PlayerPrefs.SetInt("LEVEL4", 1);
            Destroy(present);
        }
        else
        {
            PlayerPrefs.SetInt("LEVEL4", 0);
        }
    }
}
