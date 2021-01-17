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
    public int bossSpawn;

    public bool battleSpawn;
    float volumeChange = 0.1f;
    float maxVolume = 0.08f;
    float aMaxVolume = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        //music = GameObject.FindGameObjectWithTag("Music");
        
        dyn = GameObject.FindGameObjectWithTag("Player").GetComponent<DynamicMusic>();
        ambiance = GameObject.FindGameObjectWithTag("AMusic");
        aAnim = GameObject.FindGameObjectWithTag("AMusic").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        enemiesSpawned = GameObject.FindGameObjectsWithTag("Enemy").Length;

        bossSpawn = GameObject.FindGameObjectsWithTag("Boss").Length;

        if (enemiesSpawned > 0)
        {
            battleSpawn = true;
        }

        if (enemiesSpawned == 0)
        {
            battleSpawn = false;
        }

        if (bossSpawn > 0)
        {
            battleSpawn = true;
        }

        if (battleSpawn == true){
            aMus.volume -= volumeChange * Time.deltaTime;
            bMus.volume += volumeChange * Time.deltaTime;

            if (bMus.volume >= maxVolume){
                bMus.volume = maxVolume;
            }
        }

        if (battleSpawn == false)
        {
            aMus.volume += volumeChange * Time.deltaTime;
            bMus.volume -= volumeChange * Time.deltaTime;

            if (aMus.volume >= aMaxVolume)
            {
                aMus.volume = aMaxVolume;
            }
        }
    }

    public void Inactive(){
        music.SetActive(false);
    }
}
