using System.Collections.Generic;
using CollisionDetection2D.BoundingVolumes;
using OZLib;
using UnityEngine;

public class TestAABBShapes
{
    public Transform transform;
    public List<Vector3> vertices = new List<Vector3>();
    public Vector2[] worldVertices2D;
    public AABBMinMax initialBounds;
    public AABBMinMax currentBounds;
}

public class AABBBoundTestMono : MonoBehaviour
{
    public MeshFilter[] meshFilter;
    List<TestAABBShapes> testShapesList = new List<TestAABBShapes>();

    void Start()
    {
        for (int s = 0; s < meshFilter.Length; s++)
        {
            var mesh = meshFilter[s].mesh;
            var v = new List<Vector3>();
            
            mesh.GetVertices(v);
            Vector2[] vector2s = new Vector2[v.Count];
            
            TestAABBShapes shape = new TestAABBShapes();
            shape.vertices = v;
            shape.transform = meshFilter[s].transform;
            shape.worldVertices2D = vector2s;
            tempWorldVertices.Clear();
            for (int i = 0; i < shape.vertices.Count; i++)
            {
                tempWorldVertices.Add(shape.transform.TransformPoint(shape.vertices[i]));
            }
            
            OZVectorUtils.Vector3ArrayToVector2Array(tempWorldVertices.ToArray(), shape.worldVertices2D);
            shape.initialBounds = AABBUpdateMethod2.GetAABB_Bound(shape.worldVertices2D);
            shape.currentBounds = shape.initialBounds;


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

            shape.currentBounds = AABBUpdateMethod4.UpdateAABB(shape.initialBounds, shape.transform.eulerAngles.z,shape.transform.position); 
            OZDebug.DrawBoxMinMaxPoints(shape.currentBounds.min, shape.currentBounds.max, Color.red,0);

        }
        
        

    }

}
