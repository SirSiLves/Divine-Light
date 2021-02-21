using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour
{

    private LineRenderer lineRenderer;
    private Transform[] hitPoint;


    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }


    public void SetUpLine(Transform[] points)
    {
        lineRenderer.positionCount = points.Length;
        this.hitPoint = points;
    }


    private void Update()
    {
        // connect all together
        for (int i = 0; i < hitPoint.Length; i++)
        {
            lineRenderer.SetPosition(i, hitPoint[i].position);
        }
    }


}
