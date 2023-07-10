using System.Collections.Generic;
using CollisionDetection2D.BoundingVolumes;
using OZLib;
using UnityEngine;

public class TestCircleShapes
{
    public Transform transform;
    public List<Vector3> vertices = new List<Vector3>();
    public Vector2[] worldVertices2D;
}

public class CircleBoundTestMono : MonoBehaviour
{
    public MeshFilter[] meshFilter;
    List<TestCircleShapes> testShapesList = new List<TestCircleShapes>();
    
    void Start()
    {
        for (int s = 0; s < meshFilter.Length; s++)
        {
            var mesh = meshFilter[s].mesh;
            var v = new List<Vector3>();
            
            mesh.GetVertices(v);
            Vector2[] vector2s = new Vector2[v.Count];
            
            TestCircleShapes shape = new TestCircleShapes();
            shape.vertices = v;
            shape.transform = meshFilter[s].transform;
            shape.worldVertices2D = vector2s;
            tempWorldVertices.Clear();
            for (int i = 0; i < shape.vertices.Count; i++)
            {
                tempWorldVertices.Add(shape.transform.TransformPoint(shape.vertices[i]));
            }
            
            OZVectorUtils.Vector3ArrayToVector2Array(tempWorldVertices.ToArray(), shape.worldVertices2D);

            testShapesList.Add(shape);
        }
    }

    List<Vector3> tempWorldVertices = new List<Vector3>();
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
            
            OZVectorUtils.Vector3ArrayToVector2Array(tempWorldVertices.ToArray(),shape.worldVertices2D);
            var bound = CircleUpdateMethod2.GetCircle_Bound(shape.worldVertices2D);
            OZLib.OZDebug.DrawXYCircle(bound.center, bound.radius, Color.red,0);

        }
        
        

    }
}