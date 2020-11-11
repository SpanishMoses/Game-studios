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

    public int dronesStaggared;

    // Start is called before the first frame update
    void Start()
    {
        collid.enabled = false;
        for (int i = 0; i < drones.Length; i++)
        {
            dronesStaggared = drones.Length;
        }
        }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < drones.Length; i++){
            /*if (drones[0].GetComponent<BossDrone>().stopped == true && drones[1].GetComponent<BossDrone>().stopped == true)
            {
                collid.enabled = true;               
                mov.locActive = false;
                staggered = true;
            }
        }*/
            if (dronesStaggared == 0){
                staggered = true;
                collid.enabled = true;
                mov.locActive = false;
                shoot.enabled = false;
                StartCoroutine(resetDrones());
            }
        }

        if (staggered == true){
            staggerTime += Time.deltaTime;
            if (staggerTime >= maxStaggerTime){
                staggered = false;
                staggerTime = 0;
                mov.locActive = true;
                shoot.enabled = true;
                collid.enabled = false;
                //dronesStaggared = drones.Length;
            }
        }
    }

    public void Add(int num){
        dronesStaggared += num;
    }

    public void Sub(int num){
        dronesStaggared -= num;
    }

    IEnumerator resetDrones(){
        drones[0].GetComponent<BossDrone>().deathTime = 0;
        drones[1].GetComponent<BossDrone>().deathTime = 0;
        yield return new WaitForSeconds(0.01f);
    }
}
