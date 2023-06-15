using UnityEngine;



namespace CollisionDetection2D.BoundingVolumes
{
    //Section 4.2.3 -> Utilizing a fixed-size loose AABB that always encloses the object
    /*
     * You dont have to update local bounds per frame, but you have to update local bounds when the object moves
     */
    
    public static class AABBUpdateMethod1
    {
        public static AABBMinMax LocalToWorldSpace(AABBMinMax aabb, Transform transform)
        {
            Vector2 worldPos = transform.position;
            Vector2 min = aabb.min + worldPos;
            Vector2 max = aabb.max + worldPos;

            return new AABBMinMax
            {
                min = min,
                max = max
            };
        }

        public static AABBMinMax GetAABBLocalBound(Vector3[] localVertices)
        {
            int farthestIdx = 0;

            void FindFarthestPoint(Vector3[] points)
            {
                float maxDist = 0f;
                for (int i = 0; i < points.Length; i++)
                {
                    float dist = points[i].sqrMagnitude;
                    if (dist > maxDist)
                    {
                        maxDist = dist;
                        farthestIdx = i;
                    }
                }
            }

            //Step 1 find farthest point
            FindFarthestPoint(localVertices);

            //Step 2 find circle bound
            var radius = Mathf.Sqrt(localVertices[farthestIdx].sqrMagnitude);

            //Step 3 find AABB bound
            Vector2 min = new Vector2(-radius, -radius);
            Vector2 max = new Vector2(radius, radius);


            return new AABBMinMax
            {
                min = min,
                max = max
            };
        }
    }

    
    //Section 4.2.4 -> Computing a tight dynamic reconstruction from the original point set
    /*
     * You have to use world pos vertices to update
     */
    
    public static class AABBUpdateMethod2
    {
        public static AABBMinMax GetAABB_Bound(Vector3[] worldVertices)
        {
            float minProjX = float.MaxValue;
            float maxProjX = float.MinValue;
            float minProjY = float.MaxValue;
            float maxProjY = float.MinValue;

            for (int i = 0; i < worldVertices.Length; i++)
            {
                float projX = Vector2.Dot(worldVertices[i], Vector2.right);
                float projY = Vector2.Dot(worldVertices[i], Vector2.up);
                
                if (projX < minProjX) minProjX = projX;
                if (projX > maxProjX) maxProjX = projX;
                if (projY < minProjY) minProjY = projY;
                if (projY > maxProjY) maxProjY = projY;
            }
            
            Vector2 min = new Vector2(minProjX, minProjY);
            Vector2 max = new Vector2(maxProjX, maxProjY);
            
            return new AABBMinMax
            {
                min = min,
                max = max
            };
        }
    }
}