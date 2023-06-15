using UnityEngine;

namespace CollisionDetection2D.BoundingVolumes
{
    public static class Intersections
    {
        public static bool TestCircle_Circle(Circle a, Circle b)
        {
            Vector2 d = a.center - b.center;
            float dist2 = d.sqrMagnitude;
            
            float radiiSum = a.radius + b.radius;
            return dist2 <= radiiSum * radiiSum;
        }
        
        public static bool TestAABB_AABB(AABBMinMax a, AABBMinMax b)
        {
            if (a.max.x < b.min.x || a.min.x > b.max.x) return false;
            if (a.max.y < b.min.y || a.min.y > b.max.y) return false;

            return true;
        }
        
        public static bool TestAABB_AABB(AABBMinWidth a, AABBMinWidth b)
        {
            float t;
            if((t = a.min.x - b.min.x) > b.extents.x || -t > a.extents.x) return false;
            if((t = a.min.y - b.min.y) > b.extents.x || -t > a.extents.y) return false;

            return true;
        }
        
        public static bool TestAABB_AABB(AABBCenterRadius a, AABBCenterRadius b)
        {
            if(Mathf.Abs(a.center.x - b.center.x) > a.radius.x + b.radius.x) return false;
            if(Mathf.Abs(a.center.y - b.center.y) > a.radius.y + b.radius.y) return false;

            return true;
        }
    }
}


