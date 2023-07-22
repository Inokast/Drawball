using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Path : MonoBehaviour
{
    [Header("Line Rendering")]
    public LineRenderer lineRenderer;
    public EdgeCollider2D edgeCol;

    protected List<Vector2> segmentList;
    protected List<Vector2> segmentBackupList;



    public virtual void UpdateLine(Vector2 mousePos, PathMeter meter)
    {

        if (segmentList == null)
        {
            segmentList = new List<Vector2>();
            segmentBackupList = new List<Vector2>();
            SetSegment(mousePos, meter);
            return;
        }

        if (Vector2.Distance(segmentList.Last(), mousePos) > .3f) // checks if distance between the last path segment and the current mouse/touch position meets criteria
        {
            SetSegment(mousePos, meter);
        }
    }

    protected virtual void SetSegment(Vector2 segment, PathMeter meter)
    {
        meter.UseStamina(1);
        segmentList.Add(segment);

        lineRenderer.positionCount = segmentList.Count;
        lineRenderer.SetPosition(segmentList.Count - 1, segment);

        if (segmentList.Count > 1) // Checks if there is more than 1 path segment
        {
            edgeCol.points = segmentList.ToArray(); //Adds collision to new points
        }

        StartCoroutine(RemoveSegment(segment)); // Removes segment after enough time has passed       
    }

    protected IEnumerator RemoveSegment(Vector2 segment)
    {
        
        yield return new WaitForSeconds(4.0f);

        segmentList.Remove(segment);
        
        if (segmentList.Count < 1) // Checks if there are any segments remaining
        {
            Destroy(gameObject);
        }

        else
        {
            RedrawPath();
            edgeCol.points = new Vector2[segmentList.Count]; //Remakes the array of points on the line's collider with accurate segments
            edgeCol.points = segmentList.ToArray();
        }       
    }

    protected void RedrawPath() //Remakes the rendered line with accurate segments
    {
        int newSegmentCount = lineRenderer.positionCount - 1;
        Vector3[] newPathSegments = new Vector3[newSegmentCount];

        for (int i = 0; i < newSegmentCount; i++)
        {
            newPathSegments[i] = lineRenderer.GetPosition(i + 1);
        }

        lineRenderer.SetPositions(newPathSegments);
    }
}
