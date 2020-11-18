using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class List : MonoBehaviour
{
    // code help from https://www.youtube.com/watch?v=p-KjunKkuJw&ab_channel=GameDevHQ
    public List<GameObject> objects { get; set; } = new List<GameObject>();

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        GameEvents.saveInitiated += Save;
    }

    public void AddObjects(List<GameObject> objects){
        foreach(GameObject obj in objects){
            AddObjects(objects);
        }
    }
   
    void Save(){
        SaveLoad.Save<List<GameObject>>(objects, "Defeated");
    }
}
