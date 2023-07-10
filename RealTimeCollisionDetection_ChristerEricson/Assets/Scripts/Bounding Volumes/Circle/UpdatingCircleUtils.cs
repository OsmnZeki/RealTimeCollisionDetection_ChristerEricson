using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CollisionDetection2D.BoundingVolumes
{
    //Section 4.3.2 ->
    /*
     * Çalışma mantığı:
     *  Tüm vertexlerin sığabileceği bir circle bulunuyor. Bunu yapabilmek için de centera en uzak olan vertex seçiliyor radius olarak işaretleniyor.
     */
    public static class CircleUpdateMethod1
    {
        public static Circle GetCircle_Bound(Vector2[] worldVertices)
        {
            int farthestIdx = 0;
            var center = OZLib.OZMesh2DUtils.GetCenter(worldVertices);

            void FindFarthestPoint(Vector2[] points)
            {
                float maxDist = 0f;
                for (int i = 0; i < points.Length; i++)
                {
                    float dist = (center-points[i]).sqrMagnitude;
                    if (dist > maxDist)
                    {
                        maxDist = dist;
                        farthestIdx = i;
                    }
                }
            }

            //Step 1 find farthest point
            FindFarthestPoint(worldVertices);

            //Step 2 find circle bound
            var radius = Mathf.Sqrt((center-worldVertices[farthestIdx]).sqrMagnitude);
            
            return new Circle()
            {
                radius = radius,
                center = center
            };
        }
    }
    
    
    //Section 4.3.2 -> Yukarıdaki methodun daha optimize edilmiş hali Ritter90
    /*
     * Çalışma mantığı:
     * Tüm vertexlerin sığabileceği bir circle bulunuyor. Bunu yapabilmek için de centera en uzak olan vertex seçiliyor radius olarak işaretleniyor.
     */
    
    public static class CircleUpdateMethod2
    {
        public static Circle GetCircle_Bound(Vector2[] worldVertices)
        {
            //Step1 Functions
            int min, max;
            void MostSeperatedPointsOnAABB(Vector2[] point)
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

            Circle CircleFromDistantPoint(Vector2[] points)
            {
                Circle c = new Circle();
                c.center = (points[min] + points[max]) * 0.5f;
                
                var radiusSquared = ((Vector2)points[max] - c.center).sqrMagnitude;
                c.radius = Mathf.Sqrt(radiusSquared);
                return c;
            }
            
            //Step2 functions
            void RitterCircle(ref Circle c, Vector2[] points)
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
            MostSeperatedPointsOnAABB(worldVertices);
            Circle c = CircleFromDistantPoint(worldVertices);
            
            //Step 2
            RitterCircle(ref c, worldVertices);
            return c;
        }
    }
}

