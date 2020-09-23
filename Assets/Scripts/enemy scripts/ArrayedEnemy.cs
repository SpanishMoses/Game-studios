using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrayedEnemy : MonoBehaviour
{
    //float amount = 1f;
    public EnemySpawner spawn;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeAway(float amount){
        if (spawn.activated == true)
        {
            spawn.enemiesSpawned--;
        }
    }
}
