using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{

    public AudioClip main;
    public AudioClip tut;

    public AudioSource mus;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
