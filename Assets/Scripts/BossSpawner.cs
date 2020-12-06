using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawner : MonoBehaviour
{
    public GameObject boss;

    // Start is called before the first frame update
    void Start()
    {
        boss.SetActive(false); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
