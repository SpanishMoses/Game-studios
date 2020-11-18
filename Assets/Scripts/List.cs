using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class List : MonoBehaviour
{
    // code help from https://www.youtube.com/watch?v=p-KjunKkuJw&ab_channel=GameDevHQ

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public List<GameObject> objects = new List<GameObject>();
}
