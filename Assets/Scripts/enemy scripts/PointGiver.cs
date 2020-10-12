using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointGiver : MonoBehaviour
{
    public BoxCollider collid;
    public PointManager mainPoint;
    public int targetPoints;

    private void Awake()
    {
        mainPoint = GameObject.FindGameObjectWithTag("PointManager").GetComponent<PointManager>();
    }

    public void GivePoint(int points){
        mainPoint.totalPoints += points;
    }
}
