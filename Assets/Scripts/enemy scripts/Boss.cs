using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{

    public GameObject[] drones;

    public BoxCollider collid;
    public EnemyShoot shoot;
    public EnemyMove mov;

    public bool staggered;
    public float staggerTime;
    public float maxStaggerTime;

    // Start is called before the first frame update
    void Start()
    {
        collid.enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < drones.Length; i++){
            if (drones[0].GetComponent<BossDrone>().stopped == true && drones[1].GetComponent<BossDrone>().stopped == true)
            {
                collid.enabled = true;               
                mov.locActive = false;
                staggered = true;
            }
        }

        if (staggered == true){
            staggerTime += Time.deltaTime;
            if (staggerTime >= maxStaggerTime){
                staggered = false;
                staggerTime = 0;
                mov.locActive = true;
                collid.enabled = false;
            }
        }
    }
}
