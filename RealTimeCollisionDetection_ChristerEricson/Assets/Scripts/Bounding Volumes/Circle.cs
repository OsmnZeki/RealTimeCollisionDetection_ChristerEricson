using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CollisionDetection2D.BoundingVolumes
{
    public struct Circle
    {
        public Vector2 center;
        public float radius;
    }

    public static class Intersections
    {
        public static bool TestCircleCircle(Circle a, Circle b)
        {
            Vector2 d = a.center - b.center;
            float dist2 = d.sqrMagnitude;
            
            float radiiSum = a.radius + b.radius;
            return dist2 <= radiiSum * radiiSum;
        }
        
        
    }
}


