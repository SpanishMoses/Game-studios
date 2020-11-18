using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UniqueID : MonoBehaviour
{
    //help from https://www.youtube.com/watch?v=Rgxbl5uIKO0&ab_channel=GameGrind

    public string ID { get; private set; }

    // Start is called before the first frame update

    private void Awake()
    {
        ID = transform.position.sqrMagnitude + "-" + name + transform.GetSiblingIndex();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
