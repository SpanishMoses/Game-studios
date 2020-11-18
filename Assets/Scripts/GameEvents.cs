using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    //help from https://www.youtube.com/watch?v=Rgxbl5uIKO0&ab_channel=GameGrind

    public static System.Action saveInitiated;

    public static void OnSaveInitiated(){
        saveInitiated?.Invoke();
    }
}
