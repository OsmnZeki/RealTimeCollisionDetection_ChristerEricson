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
}


