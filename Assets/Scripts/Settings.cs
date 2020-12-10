using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class Settings : MonoBehaviour
{
    //code from https://www.youtube.com/watch?v=YOaYQrN1oYQ&t=157s&ab_channel=Brackeys

    public AudioMixer mixer;
    public MouseLook mouse;
    public Slider slide;
    public TMP_Text senseNum;

    public Dropdown resolutionDropdown;

    Resolution[] resolutions;

    private void Start()
    {
        resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++){
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height){
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    public void SetVolume(float volume){
        mixer.SetFloat("Volume", volume);
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

    public void SetResolution(int resolutionIndex){
        Resolution resolution = resolutions[resolutionIndex];

        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void Update()
    {
        senseNum.text = "" + slide.value;
        
    }
}
