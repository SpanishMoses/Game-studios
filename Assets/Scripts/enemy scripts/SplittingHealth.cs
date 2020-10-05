using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplittingHealth : MonoBehaviour
{
    //enemy spawning cdoe from Dan from food friendz

    public GameObject parent;
    public Transform leftPt;
    public Transform rightPt;


    public GameObject littleBois;

    public float time;
    public float maxTimer;

    public EnemyHealth health;

    private void Update()
    {
        if (health.health <= health.minHealth)
        {
            time += Time.deltaTime;
            if (time >= maxTimer){
                Instantiate(littleBois, leftPt.position, Quaternion.identity);
                Instantiate(littleBois, rightPt.position, Quaternion.identity);
                time = 0;
                
            }
            
            
        }
    }

    void spawn(){
        
    }

    
    
}
