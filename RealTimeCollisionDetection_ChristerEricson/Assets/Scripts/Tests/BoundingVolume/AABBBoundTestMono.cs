using System.Collections.Generic;
using CollisionDetection2D.BoundingVolumes;
using OZLib;
using UnityEngine;

public class AABBBoundTestMono : MonoBehaviour
{
    public MeshFilter meshFilter;

    List<Vector3> vertices = new List<Vector3>();
    List<Vector3> worldVertices = new List<Vector3>();
    AABBMinMax localBound;
    
    void Start()
    {
        var m = meshFilter.mesh;
        
        m.GetVertices(vertices);
    }

    void Update()
    {
        worldVertices.Clear();
        for (int i = 0; i < vertices.Count; i++)
        {
            worldVertices.Add(meshFilter.transform.TransformPoint(vertices[i]));
        }
        localBound = AABBUpdateMethod2.GetAABB_Bound(worldVertices.ToArray());
        OZDebug.DrawBoxMinMaxPoints(localBound.min, localBound.max, Color.red, 0);
    }

}
