using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondLineRenderer : MonoBehaviour
{
    public LineRenderer line;
    public Transform pos1; // 위
    public Transform pos2;  // 집게 위치

    void Start()
    {
        line.positionCount = 2;
    }

    // Update is called once per frame
    void Update()
    {
        line.SetPosition(0, pos1.position);
        line.SetPosition(1, pos2.position);
    }
}
