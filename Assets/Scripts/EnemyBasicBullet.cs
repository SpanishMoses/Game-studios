﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBasicBullet : MonoBehaviour
{
    //https://www.youtube.com/watch?v=_Z1t7MNk0c4&t=682s

    public float speed;

    private Transform player;
    private Vector3 target;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        target = new Vector3(player.position.x, player.position.y, player.position.z);
    }

    // Update is called once per frame
    void Update()
    {
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

            if (transform.position.x == target.x && transform.position.y == target.y && transform.position.z == target.z)
            {
                DestroyProjectile();

                Destroy(gameObject, 9f);
            }
    }

    void DestroyProjectile()
    {
        Destroy(gameObject);
    }
 }
