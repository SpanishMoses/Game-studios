using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleItemSet : MonoBehaviour
{
    //help from https://www.youtube.com/watch?v=Rgxbl5uIKO0&ab_channel=GameGrind

    public HashSet<string> CollectedItems { get; private set; } = new HashSet<string>();
}
