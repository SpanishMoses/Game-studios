using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class Settings : MonoBehaviour
{
    //code from https://www.youtube.com/watch?v=YOaYQrN1oYQ&t=157s&ab_channel=Brackeys
    //volume help from https://forum.unity.com/threads/solved-playerprefs-save-for-my-audio-problem.478091/

    public AudioMixer mixer;
    public MouseLook mouse;
    public Slider slide;
    public Slider volSlide;
    public TMP_Text senseNum;

    public Toggle tog;
    public Toggle tog2;
    public Toggle tog3;
    public bool enableCon;

    public bool enableSpecial;

    //public Dropdown resolutionDropdown;

    //Resolution[] resolutions;

    private void Start()
    {
        /*enableCon = PlayerPrefs.GetInt("Confetti", 0) > 0;

        if (enableCon == true)
        {
            EnableConfetti(true);
        }

        if (enableCon == false)
        {
            EnableConfetti(false);
        }

        //resolutions = Screen.resolutions;

        //resolutionDropdown.ClearOptions();

        //List<string> options = new List<string>();

        //int currentResolutionIndex = 0;

        /*for (int i = 0; i < resolutions.Length; i++){
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height){
                currentResolutionIndex = i;
            }
        }*/

        /*resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();*/
        
    }

    public void SetVolume(float volume){
        mixer.SetFloat("Volume", volume);
        mixer.GetFloat("Volume", out volume);
        PlayerPrefs.SetFloat("Volume", volume);
        volSlide.value = volume;
    }

    public void SetSensitivity(float sensitivity){
        mouse.mouseSensitivity = slide.value;
        PlayerPrefs.SetFloat("Sense", slide.value);
    }

    public void SetQuality(int qualityIndex){
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetFullscreen(bool isFullScreen){
        Screen.fullScreen = isFullScreen;
    }

    /*public void SetResolution(int resolutionIndex){
        Resolution resolution = resolutions[resolutionIndex];

        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }*/

    public void Update()
    {
        senseNum.text = "" + mouse.mouseSensitivity;

        slide.value = mouse.mouseSensitivity;

        /*int yesConfetti;
        yesConfetti = enableCon ? 1 : 0;

        if (enableCon == true)
        {
            PlayerPrefs.SetInt("Confetti", 1);
            Debug.Log("worked");
        }
        else
        {
            PlayerPrefs.SetInt("Confetti", 0);
        }*/


    }

    public void EnableConfetti(bool confettiYes){
        
        if (confettiYes == true)
        {
            mouse.enableCon = true;
        }
        else
        {
            mouse.enableCon = false;
        }

    }

    public void EnableCandy(bool candyYes){
        if (candyYes == true){
            mouse.enableCandy = true;
        } else{
            mouse.enableCandy = false;
        }
    }

    public void Special(bool specialYes){
        if (specialYes == true){
            mouse.enableSpecial = true;
        } else{
            mouse.enableSpecial = false;
        }
    }
}
