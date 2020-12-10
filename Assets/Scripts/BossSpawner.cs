using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawner : MonoBehaviour
{
    public GameObject boss;
    public GameObject[] spawners;

    // Start is called before the first frame update
    void Start()
    {
        boss.SetActive(false);
        for (int i = 0; i < spawners.Length; i++)
        {
            spawners[i].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
