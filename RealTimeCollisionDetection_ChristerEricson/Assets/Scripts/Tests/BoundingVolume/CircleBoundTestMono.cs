using System.Collections.Generic;
using CollisionDetection2D.BoundingVolumes;
using OZLib;
using UnityEngine;

public class CircleBoundTestMono : MonoBehaviour
{
    public MeshFilter meshFilter;
    public void Start()
    {
        var m = meshFilter.mesh;

        List<Vector3> vertices = new List<Vector3>();
        m.GetVertices(vertices);
        for (int i = 0; i < vertices.Count; i++)
        {
        //    vertices[i] = meshFilter.transform.TransformPoint(vertices[i]);
        }

        var bound = BoundingCircleUtils.Ritter90ComputeCircleBound(vertices.ToArray());
        OZDebug.DrawXYCircle(bound.center, bound.radius, Color.red, 10);
    }
}