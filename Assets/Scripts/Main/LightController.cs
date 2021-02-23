using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(LineRenderer))]
public class LightController : MonoBehaviour
{

    private const float delay = 0.02f;
    private const float infinity = 999f;
    private const float defaultRayDistance = 100f;

    private Vector2 startPoint, startDirection;
    private LineRenderer lineRenderer;

    private List<Vector3> connectedPoints = new List<Vector3>();
    private List<Vector3> delayedDrawnPoints = new List<Vector3>();


    private void OnEnable()
    {
        CollectPoints();
        StartCoroutine("DrawLight");
    }


    IEnumerator DrawLight() // name reference
    {
        delayedDrawnPoints.Clear();

        foreach (Vector2 point in connectedPoints.ToArray())
        {
            delayedDrawnPoints.Add(point);

            lineRenderer.positionCount = delayedDrawnPoints.Count;
            lineRenderer.SetPositions(delayedDrawnPoints.ToArray());

            yield return new WaitForSeconds(delay);
        }
    }


    private void CollectPoints()
    {
        startPoint = transform.position;
        SetDirection();

        lineRenderer = transform.GetComponent<LineRenderer>();

        RaycastHit2D hitObject = Physics2D.Raycast(startPoint, (startDirection - startPoint).normalized, defaultRayDistance);        

        connectedPoints.Clear();
        connectedPoints.Add(startPoint);

        if (hitObject)
        {
            ReflectFurther(startPoint, hitObject);
        }
        else
        {
            connectedPoints.Add(hitObject.point + startDirection * defaultRayDistance);
        }
    }


    private void ReflectFurther(Vector2 nextStartPoint, RaycastHit2D hitObject)
    {
        connectedPoints.Add(hitObject.point);

        // dont go further if object has to be destroyed
        if (ValidateDestroy(hitObject)) { return; }


        Vector2 fromDirection = (hitObject.point - nextStartPoint).normalized;
        Vector2 newDirection = Vector2.Reflect(fromDirection, hitObject.normal);

        var newHitObject = Physics2D.Raycast(hitObject.point + (newDirection * 0.0001f), newDirection * 100, defaultRayDistance);

        if (newHitObject)
        {
            ReflectFurther(hitObject.point, newHitObject);
        }
        else
        {
            connectedPoints.Add(hitObject.point + newDirection * defaultRayDistance);
        }
    }


    private bool ValidateDestroy(RaycastHit2D hitObject)
    {
        switch(hitObject.transform.gameObject.name)
        {
            case "Destroy":
                AddToDestroy(hitObject);
                return true;
            case "Block":
                return true;
            default:
                return false;
        }
    }


    private static void AddToDestroy(RaycastHit2D hitObject)
    {
        FindObjectOfType<PlayerChanger>().GetPiecesToDestroy().Add(hitObject.transform.parent.gameObject.GetComponent<Piece>());
    }


    private void SetDirection()
    {
        float degrees = transform.parent.rotation.eulerAngles.z;

        switch (degrees)
        {
            case 0f:
                startDirection = new Vector2(transform.parent.position.x, transform.parent.position.y + infinity);
                break;
            case 90f:
                startDirection = new Vector2(transform.parent.position.x - infinity, transform.parent.position.y);
                break;
            case 180f:
                startDirection = new Vector2(transform.parent.position.x, transform.parent.position.y - infinity);
                break;
            case 270f:
                startDirection = new Vector2(transform.parent.position.x + infinity, transform.parent.position.y);
                break;
            default:
                Piece piece = transform.GetComponentInParent<Piece>();
                throw new Exception("Could not find the right dregrees for " + piece.name);
        }
    }


}
