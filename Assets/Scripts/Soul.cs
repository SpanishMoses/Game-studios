using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soul : MonoBehaviour
{
    float speed = 5f;

    private void Start()
    {
        
    }

    private void Update()
    {
        Vector3 pos = transform.position;
        Vector3 velocity = new Vector3(0, speed * Time.deltaTime);
        pos += transform.rotation * velocity;
        transform.position = pos;
    }
}
