using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartArena : MonoBehaviour
{

    public GameObject arena;
    public GameObject text1;
    public GameObject text2;

    public GameObject rock1;
    public GameObject rock2;

    public GameObject ambMus;
    public GameObject battleMus;

    public bool hasPassed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (hasPassed == true){
            arena.SetActive(true);
            text1.SetActive(true);
            text2.SetActive(true);
            rock1.SetActive(true);
            rock2.SetActive(true);
            ambMus.SetActive(false);
            battleMus.SetActive(true);
        }
    }
}
