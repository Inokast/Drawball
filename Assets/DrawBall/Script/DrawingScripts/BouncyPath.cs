using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BouncyPath : Path // using Class Inheritance
{
    public override void UpdateLine(Vector2 mousePos, PathMeter meter)
    {

        if (segmentList == null)
        {
            segmentList = new List<Vector2>();
            segmentBackupList = new List<Vector2>();
            SetSegment(mousePos, meter);
            return;
        }

        if (Vector2.Distance(segmentList.Last(), mousePos) > .7f && meter.currentStamina >= 2) // Different value from main path
        {
            SetSegment(mousePos, meter);
        }
    }

    protected override void SetSegment(Vector2 segment, PathMeter meter)
    {
        meter.UseStamina(2); // Different value from main path
        segmentList.Add(segment);

        lineRenderer.positionCount = segmentList.Count;
        lineRenderer.SetPosition(segmentList.Count - 1, segment);

        if (segmentList.Count > 1) // Checks if there is more than 1 path segment
        {
            edgeCol.points = segmentList.ToArray(); //Adds collision to new points
        }

        StartCoroutine(RemoveSegment(segment)); // Removes segment after enough time has passed       
    }
}
