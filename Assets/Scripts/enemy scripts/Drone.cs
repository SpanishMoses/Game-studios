using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone : MonoBehaviour
{

    private float latestDirectionChangeTime;
    private readonly float directionChangeTime = 2f;
    public float characterVelocity;
    private Vector3 movementDirection;
    private Vector3 movementPerSecond;

    // Start is called before the first frame update
    void Start()
    {
        latestDirectionChangeTime = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - latestDirectionChangeTime > directionChangeTime)
        {
            latestDirectionChangeTime = Time.time;
            calcuateNewMovementVector();
        }
        transform.position = new Vector3(transform.position.x + (movementPerSecond.x * Time.deltaTime),
    transform.position.y + (movementPerSecond.y * Time.deltaTime), transform.position.z + (movementPerSecond.z * Time.deltaTime));
    }

    void calcuateNewMovementVector()
    {
        //create a random direction vector with the magnitude of 1, later multiply it with the velocity of the enemy
        movementDirection = new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f)).normalized;
        movementPerSecond = movementDirection * characterVelocity;
    }

    private void OnCollisionEnter(Collision collision)
    {
        calcuateNewMovementVector();
    }
}

