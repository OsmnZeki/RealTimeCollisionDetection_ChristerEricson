using System.Collections.Generic;
using CollisionDetection2D.BoundingVolumes;
using OZLib;
using UnityEngine;

public class TestShapes
{
    public Transform transform;
    public List<Vector3> vertices = new List<Vector3>();
    public Vector2[] worldVertices2D;
}

public class AABBBoundTestMono : MonoBehaviour
{
    public MeshFilter[] meshFilter;
    List<TestShapes> testShapesList = new List<TestShapes>();

    List<Vector3> tempWorldVertices = new List<Vector3>();
    
    void Start()
    {
        for (int i = 0; i < meshFilter.Length; i++)
        {
            var mesh = meshFilter[i].mesh;
            var v = new List<Vector3>();
            
            mesh.GetVertices(v);
            Vector2[] vector2s = new Vector2[v.Count];
            testShapesList.Add(new TestShapes()
            {
                vertices = v,
                worldVertices2D = vector2s,
                transform = meshFilter[i].transform
            });
        }
    }

    void Update()
    {
        for (int s = 0; s < testShapesList.Count; s++)
        {
            var shape = testShapesList[s];
            
            tempWorldVertices.Clear();
            for (int i = 0; i < shape.vertices.Count; i++)
            {
                tempWorldVertices.Add(shape.transform.TransformPoint(shape.vertices[i]));
            }
        
            OZVectorUtils.Vector3ArrayToVector2Array(tempWorldVertices.ToArray(), shape.worldVertices2D);
            var hullPoints = OZMesh2DUtils.GetConvexHullPoints(shape.worldVertices2D);
            OZDebug.DrawPath(hullPoints, Color.red,0);
        }
        
        

    }

}
