using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicMusic : MonoBehaviour
{
    public bool encounterBegan;
    public DynamicMusic dyn;
    public Animator anim;
    public GameObject music;
    public GameObject[] enemies;
    public int enemiesSpawned;
    public GameObject ambiance;
    public Animator aAnim;
    public AudioSource aMus;
    public AudioSource bMus;

    // Start is called before the first frame update
    void Start()
    {
        music = GameObject.FindGameObjectWithTag("Music");
        anim = GameObject.FindGameObjectWithTag("Music").GetComponent<Animator>();
        dyn = GameObject.FindGameObjectWithTag("Player").GetComponent<DynamicMusic>();
        ambiance = GameObject.FindGameObjectWithTag("AMusic");
        aAnim = GameObject.FindGameObjectWithTag("AMusic").GetComponent<Animator>();
        bMus.mute = true;
    }

    // Update is called once per frame
    void Update()
    {
        enemiesSpawned = GameObject.FindGameObjectsWithTag("Enemy").Length;

        if (enemiesSpawned > 0)
        {
            aMus.mute = true;
            bMus.mute = false;
        }

        if (enemiesSpawned == 0)
        {
            aMus.mute = false;
            bMus.mute = true;
        }
    }

    public void Inactive(){
        music.SetActive(false);
    }
}
