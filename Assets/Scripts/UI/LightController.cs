using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(LineRenderer))]
public class LightController : MonoBehaviour
{

    private const int infinity = 999;
    private const int maxReflections = 100;

    private int currentReflections = 0;
    private int defaultRayDistance = 10;

    private Vector2 startPoint, directionPoint;
    private List<Vector3> connectedPoints;
    private LineRenderer lineRenderer;


    private void Start()
    {
        startPoint = transform.position;
        SetDirection();

        connectedPoints = new List<Vector3>();
        lineRenderer = transform.GetComponent<LineRenderer>();
    }

    private void SetDirection()
    {
        float degrees = transform.parent.rotation.eulerAngles.z;

        switch (degrees)
        {
            case 0f:
                directionPoint = new Vector2(transform.parent.position.x, transform.parent.position.y + infinity);
                break;
            case 90f:
                directionPoint = new Vector2(transform.parent.position.x - infinity, transform.parent.position.y);
                break;
            case 180f:
                directionPoint = new Vector2(transform.parent.position.x, transform.parent.position.y - infinity);
                break;
            case 270f:
                directionPoint = new Vector2(transform.parent.position.x + infinity, transform.parent.position.y);
                break;
            default:
                Piece piece = transform.GetComponentInParent<Piece>();
                throw new Exception("Could not find the right dregrees for " + piece.name);
        }
    }

    private void Update()
    {
        RaycastHit2D hitObject = Physics2D.Raycast(startPoint, (directionPoint - startPoint).normalized, defaultRayDistance);

        currentReflections = 0;
        connectedPoints.Clear();
        connectedPoints.Add(startPoint);

        if (hitObject)
        {
            ReflectFurther(startPoint, hitObject);
        }
        else
        {
            connectedPoints.Add(startPoint + (directionPoint - startPoint).normalized * infinity);
        }

        lineRenderer.positionCount = connectedPoints.Count;
        lineRenderer.SetPositions(connectedPoints.ToArray());
    }

    private void ReflectFurther(Vector2 nextStartPoint, RaycastHit2D hitObject)
    {

        print(hitObject.transform.GetComponentInParent<Piece>().name);

        if (currentReflections > maxReflections) return;

        connectedPoints.Add(hitObject.point);
        currentReflections++;

        Vector2 inDirection = (hitObject.point - nextStartPoint).normalized;
        Vector2 newDirection = Vector2.Reflect(inDirection, hitObject.normal);

        var newHitData = Physics2D.Raycast(hitObject.point + (newDirection * 0.0001f), newDirection * 100, defaultRayDistance);
        if (newHitData)
        {
            ReflectFurther(hitObject.point, newHitData);
        }
        else
        {
            connectedPoints.Add(hitObject.point + newDirection * defaultRayDistance);
        }

    }
}
