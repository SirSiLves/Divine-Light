using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(LineRenderer))]
public class ReflectRays : MonoBehaviour
{
    int maxReflections = 100;
    int currentReflections = 0;
    int defaultRayDistance = 100;

    Vector2 startPoint, direction;
    List<Vector3> Points;
    LineRenderer lineRenderer;


    void Start()
    {
        direction = new Vector2(0, 0.06f);
        print(transform.parent.position);
        print(transform.position);

        Points = new List<Vector3>();
        lineRenderer = transform.GetComponent<LineRenderer>();

        startPoint = transform.position;
    }

    private void Update()
    {
        var hitData = Physics2D.Raycast(startPoint, (direction - startPoint).normalized, defaultRayDistance);

        currentReflections = 0;
        Points.Clear();
        Points.Add(startPoint);

        if (hitData)
        {
            ReflectFurther(startPoint, hitData);
        }
        else
        {
            Points.Add(startPoint + (direction - startPoint));
        }

        lineRenderer.positionCount = Points.Count;
        lineRenderer.SetPositions(Points.ToArray());
    }

    private void ReflectFurther(Vector2 origin, RaycastHit2D hitData)
    {
        if (currentReflections > maxReflections) return;

        Points.Add(hitData.point);
        currentReflections++;

        Vector2 inDirection = (hitData.point - origin).normalized;
        Vector2 newDirection = Vector2.Reflect(inDirection, hitData.normal);

        var newHitData = Physics2D.Raycast(hitData.point + (newDirection), newDirection, defaultRayDistance);
        if (newHitData)
        {
            ReflectFurther(hitData.point, newHitData);
        }
        else
        {
            Points.Add(hitData.point + newDirection * defaultRayDistance);
        }
    }
}
