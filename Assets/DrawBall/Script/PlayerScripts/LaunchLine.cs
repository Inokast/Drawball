using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LaunchLine : MonoBehaviour
{   
    private LineRenderer lineRenderer;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    public void RenderLine(Vector3 startPoint, Vector3 endPoint) // creates a visual of line from Vector3 coordinates
    {
        lineRenderer.positionCount = 2;
        Vector3[] points = new Vector3[2];
        points[0] = startPoint;
        points[1] = endPoint;

        lineRenderer.SetPositions(points);
    }

    public void EndLine() // Gets rid of rendered line
    {
        lineRenderer.positionCount = 0;
    }
}
