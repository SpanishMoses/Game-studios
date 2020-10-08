﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankEnemy : MonoBehaviour
{
    public EnemyMove enemy;
    public GameObject rumbling;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(send());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void sendWave(){
        Vector3 direction = enemy.playerLoc.position - transform.position;
        GameObject grenadeInstance = Instantiate(rumbling, transform.position, Quaternion.identity);
        grenadeInstance.transform.forward = direction.normalized;
        grenadeInstance.GetComponent<Rigidbody>().AddForce(direction.normalized * 10f, ForceMode.Impulse);
    }

    IEnumerator send(){
        yield return new WaitForSeconds(3);
        sendWave();
        StartCoroutine(send());
    }
}