using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class Settings : MonoBehaviour
{
    public AudioMixer mixer;
    public MouseLook mouse;
    public Slider slide;
    public TMP_Text senseNum;
    public TMP_Text volNum;

    public void SetVolume(float volume){
        mixer.SetFloat("Volume", volume);
        volNum.text = "" + volume;
    }

    public void SetSensitivity(float sensitivity){
        mouse.mouseSensitivity = slide.value;
        PlayerPrefs.SetFloat("Sense", slide.value);
    }

    public void Update()
    {
        senseNum.text = "" + slide.value;
        
    }
}
