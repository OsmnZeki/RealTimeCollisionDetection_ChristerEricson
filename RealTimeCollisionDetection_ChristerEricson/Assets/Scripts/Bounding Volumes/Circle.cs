using UnityEngine;

namespace CollisionDetection2D.BoundingVolumes
{
    public struct Circle
    {
        public Vector2 center;
        public float radius;
    }

    public static class BoundingCircleUtils
    {
        public static Circle Ritter90ComputeCircleBound(Vector3[] points)
        {
            //Step1 Functions
            int min, max;
            void MostSeperatedPointsOnAABB(Vector3[] point)
            {
                int minX = 0, maxX = 0, minY = 0, maxY = 0;
                for (int i = 0; i < point.Length; i++)
                {
                    if (point[i].x < point[minX].x) minX = i;
                    if (point[i].x > point[maxX].x) maxX = i;
                    if (point[i].y < point[minY].y) minY = i;
                    if (point[i].y > point[maxY].y) maxY = i;
                }
                
                float dist2x = (point[maxX] - point[minX]).sqrMagnitude;
                float dist2y = (point[maxY] - point[minY]).sqrMagnitude;
                
                if(dist2y > dist2x)
                {
                    min = minY;
                    max = maxY;
                }
                else
                {
                    min = minX;
                    max = maxX;
                }
            }

            Circle CircleFromDistantPoint(Vector3[] points)
            {
                Circle c = new Circle();
                c.center = (points[min] + points[max]) * 0.5f;
                
                var radiusSquared = ((Vector2)points[max] - c.center).sqrMagnitude;
                c.radius = Mathf.Sqrt(radiusSquared);
                return c;
            }
            
            //Step2 functions
            void RitterCircle(ref Circle c, Vector3[] points)
            {
                for (int i = 0; i < points.Length; i++)
                {
                    Vector2 p = points[i];
                    Vector2 d = p - c.center;
                    float dist2 = d.sqrMagnitude;
                    
                    if(dist2 > c.radius * c.radius)
                    {
                        float dist = Mathf.Sqrt(dist2);
                        float newRadius = (c.radius + dist) * 0.5f;
                        float k = (newRadius - c.radius) / dist;
                        c.radius = newRadius;
                        c.center += d * k;
                    }
                }
            }
            
            //Step 1
            MostSeperatedPointsOnAABB(points);
            Circle c = CircleFromDistantPoint(points);
            
            //Step 2
            RitterCircle(ref c, points);
            return c;
        }
    }
    
}


