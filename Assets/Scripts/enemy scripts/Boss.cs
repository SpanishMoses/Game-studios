using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public GameObject rumbling;
    public GameObject[] drones;
    public GameObject spawnPoint;

    public BoxCollider collid;
    public EnemyShoot shoot;
    public EnemyMove mov;

    public bool staggered;
    public float staggerTime;
    public float maxStaggerTime;

    public bool canRumble;
    public float rumbleTime;
    public float maxRumbleTime;

    public int dronesStaggared;

    // Start is called before the first frame update
    void Start()
    {
        canRumble = true;
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
                canRumble = false;
                rumbleTime = 0;
                StartCoroutine(resetDrones());
            }
        }

        if (canRumble == true){
            rumbleTime += Time.deltaTime;
            if (rumbleTime >= maxRumbleTime){
                sendWave();
                rumbleTime = 0;
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

    void sendWave()
    {
        Vector3 direction = mov.playerLoc.position - transform.position;
        GameObject grenadeInstance = Instantiate(rumbling, spawnPoint.transform.position, Quaternion.identity);
        grenadeInstance.transform.forward = direction.normalized;
        grenadeInstance.GetComponent<Rigidbody>().AddForce(direction.normalized * 25f, ForceMode.Impulse);
    }

    IEnumerator resetDrones(){
        drones[0].GetComponent<BossDrone>().deathTime = 0;
        drones[1].GetComponent<BossDrone>().deathTime = 0;
        yield return new WaitForSeconds(0.01f);
    }

    
}
