using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class powerfade : MonoBehaviour
{

    public float time;
    public float maxTime;

    public SpriteRenderer sprite;

    public float spriteFade;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        if (time > 15){
            sprite.color -= new Color(0, 0, 0, spriteFade * Time.deltaTime);
        }

        if (time >= maxTime){
            Destroy(gameObject);
        }
    }
}
