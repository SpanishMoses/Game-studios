using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockwaveExpand : MonoBehaviour
{
    //code from https://www.youtube.com/watch?v=Lnrdz7vXVCI&ab_channel=DesignandDeploy

    public float radius = .7f;
    public float speed;
    public ParticleSystem part;
    public float timeVar = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeVar += Time.deltaTime;
        if (timeVar > .2){
            timeVar = 0;
            radius += 1f;
        }
        ParticleSystem.ShapeModule psShape = part.shape;
        psShape.radius = radius;
        Destroy(gameObject, 10f);
    }
}
