using System.Collections.Generic;
using OZLib;
using UnityEngine;

namespace CollisionDetection2D.BoundingVolumes
{
    //Section 4.2.3 -> Utilizing a fixed-size loose AABB that always encloses the object
    /*
     * Çalışma mantığı:
     *  Tüm vertexlerin sığabileceği bir circle bulunuyor. Bunu yapabilmek için de centera en uzak olan vertex seçiliyor radius olarak işaretleniyor.
     *  Böylelikle şeklin tüm rotasyonlu halinin sığabileceği bir circle olmuş oluyor
     *  Bu circledaki verilere bakarak AABB bound bulunuyor.
     *  Güncellemek için ise rotasyondan bağımsız bir hale geldiği için sadece boundun pozisyonu güncelleniyor.
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

        public static AABBMinMax GetAABBLocalBound(Vector2[] localVertices)
        {
            int farthestIdx = 0;

            void FindFarthestPoint(Vector2[] points)
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
     * Çalışma mantığı:
     *  Her frame vertexleri gezerek, her direction için min ve max uzaklıkları bulunuyor.
     */

    public static class AABBUpdateMethod2
    {
        public static AABBMinMax GetAABB_Bound(Vector2[] worldVertices)
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

    //Section 4.2.5 -> Computing a tight dynamic reconstruction using hill climbing
    /*
     * TODO: anlayamadım eksik kaldı
     *
     *  Algoritma konveks bir şekil olduğu zaman çalışıyor. Eğer ki konveks bir şekil değilse Convex Hull üzerinden yapılmalı
     *  Algoritma konveks şekiller için daha uygun belki onlar kullandıldığında daha optimize çalışır
     * 
     * Çalışma mantığı:
     *  Her direction üzerindeki en uzak vertexleri cacheliyor. Böylelikle sürekli olarak en baştan en uzak noktayı bulmaya gerek kalmıyor.
     *  Update ederken her direction için cachelediğin vertexlerin komşularına bakılıyor. Eğer ki vertexin komşusu daha uzaksa o vertex cachele ediliyor.
     *  Bu işlemi yaparken AABB boundları da güncelleniyor.
     * 
     * Problem:
     *  1. Konkav bir şekil olduğu zaman Convex Hull algoritması çalıştığı için direkt Method2 yapılırsa sanki daha iyi olur gibi
     */
    
    public static class AABBUpdateMethod3
    {
        public class HillClimbData
        {
            public int minXIdx;
            public int maxXIdx;
            public int minYIdx;
            public int maxYIdx;

            public Dictionary<int, List<int>> adjencencyList;

            public HillClimbData()
            {
                adjencencyList = new Dictionary<int, List<int>>();
            }

        }

        public static AABBMinMax GetInitialBound(Vector2[] worldVertices, out HillClimbData hillClimbData)
        {
            hillClimbData = new HillClimbData();
            var hullPoints = OZMesh2DUtils.GetConvexHullPoints(worldVertices);

            for (int i = 0; i < hullPoints.Length; i++)
            {
                var nextIdx = (i + 1) % hullPoints.Length;
                var prevIdx = (i - 1 + hullPoints.Length) % hullPoints.Length;
                
                if (!hillClimbData.adjencencyList.ContainsKey(i))
                {
                    hillClimbData.adjencencyList.Add(i, new List<int>()
                    {
                        prevIdx,nextIdx
                    });
                }
            }

            return new AABBMinMax();
        }
    }
}