using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UINoise : MonoBehaviour
{

    public AudioSource noise;
    public AudioClip hover;
    public AudioClip enterSound;

    public void hoverNow()
    {
        noise.clip = hover;
        noise.Play();
    }

    public void nowClick()
    {
        noise.clip = enterSound;
        noise.Play();
    }
}
