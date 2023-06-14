using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CollisionDetection2D.BoundingVolumes
{
    public struct AABBMinMax
    {
        public Vector2 min;
        public Vector2 max;
    }

    public struct AABBMinWidth
    {
        public Vector2 min;
        public Vector2 extents;
    }

    public struct AABBCenterRadius
    {
        public Vector2 center;
        public Vector2 radius;
    }

    public static class AABBUtils
    {
        public static bool TestAABB(AABBMinMax a, AABBMinMax b)
        {
            if (a.max.x < b.min.x || a.min.x > b.max.x) return false;
            if (a.max.y < b.min.y || a.min.y > b.max.y) return false;

            return true;
        }
        
        public static bool TestAABB(AABBMinWidth a, AABBMinWidth b)
        {
            float t;
            if((t = a.min.x - b.min.x) > b.extents.x || -t > a.extents.x) return false;
            if((t = a.min.y - b.min.y) > b.extents.x || -t > a.extents.y) return false;

            return true;
        }
        
        public static bool TestAABB(AABBCenterRadius a, AABBCenterRadius b)
        {
            if(Mathf.Abs(a.center.x - b.center.x) > a.radius.x + b.radius.x) return false;
            if(Mathf.Abs(a.center.y - b.center.y) > a.radius.y + b.radius.y) return false;

            return true;
        }
    }
}


