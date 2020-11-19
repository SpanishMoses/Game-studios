using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoints : MonoBehaviour
{
    //help from https://www.youtube.com/watch?v=Rgxbl5uIKO0&ab_channel=GameGrind

    public void Save(){
        GameEvents.OnSaveInitiated();
    }
}
