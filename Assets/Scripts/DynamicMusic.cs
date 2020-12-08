using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicMusic : MonoBehaviour
{
    public DynamicMusic dyn;
    public Animator anim;
    public GameObject music;
    public GameObject[] enemies;
    public int enemiesSpawned;
    public GameObject ambiance;

    // Start is called before the first frame update
    void Start()
    {
        music = GameObject.FindGameObjectWithTag("Music");
        anim = GameObject.FindGameObjectWithTag("Music").GetComponent<Animator>();
        dyn = GameObject.FindGameObjectWithTag("Player").GetComponent<DynamicMusic>();
        music.SetActive(false);
        ambiance.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        enemiesSpawned = GameObject.FindGameObjectsWithTag("Enemy").Length;

        if (enemiesSpawned > 0)
        {
            music.SetActive(true);
            ambiance.SetActive(false);
        }

        if (enemiesSpawned == 0)
        {
            anim.SetTrigger("Switch");
            ambiance.SetActive(true);
        }
    }

    public void Inactive(){
        music.SetActive(false);
    }
}
