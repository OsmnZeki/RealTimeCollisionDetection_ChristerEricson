using System;
using System.Collections.Generic;
using UnityEngine;

namespace OZLib
{
    public static class OZMesh2DUtils
    {
        class VertexComparer : IComparer<Vector2>
        {
            Vector2 center;

            public VertexComparer(Vector2 center)
            {
                this.center = center;
            }

            public int Compare(Vector2 x, Vector2 y)
            {
                float angleA = Vector2.SignedAngle(x - center, Vector2.up);
                float angleB = Vector2.SignedAngle(y - center, Vector2.up);

                if (angleA < angleB)
                {
                    return -1;
                }
                else if (angleA > angleB)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
        }

        public static Vector2 GetCenter(Vector2[] points)
        {
            Vector2 center = Vector2.zero;
            for (int i = 0; i < points.Length; i++)
            {
                center += points[i];
            }
            center /= points.Length;
            return center;
        }
        


        //Convex hull functions

        static int IsClockwise(Vector2 a, Vector2 b, Vector2 c)
        {
            var v1 = b - a;
            var v2 = c - b;
            
            float crossProduct = v1.x * v2.y - v1.y * v2.x;
            if (crossProduct < 0) return -1; //clockwise
            if (crossProduct > 0) return 1; //counter-clockwise
            return 0; //colinear
        }
        
        static int SortVectorsCounterClockwise(Vector2 a, Vector2 b, Vector2 pivot)
        {
            Vector2 pointA = a - pivot;
            Vector2 pointB = b - pivot;

            float angleA = Mathf.Atan2(pointA.y, pointA.x);
            float angleB = Mathf.Atan2(pointB.y, pointB.x);

            if (angleA < angleB)
            {
                return -1;
            }
            if (angleA > angleB)
            {
                return 1;
            }
            return 0;
        }

        
        static Stack<Vector2> vectorStack = new Stack<Vector2>();
        
        //TODO: 3d pointleri z=0 gibi kabul edildiğinde doğru çalışmıyor ama gerçek 2d pointler için doğru çalışıyor
        public static Vector2[] GetConvexHullPoints(Vector2[] points)
        {
            int n = points.Length;
            if (n < 3) return null;

            Vector2 minYPos = points[0];
            for (int i = 1; i < n; i++)
            {
                if (points[i].y < minYPos.y || (Mathf.Approximately(points[i].y,minYPos.y)  && points[i].x < minYPos.x))
                {
                    minYPos = points[i];
                }
            }
            
            Array.Sort(points, (a, b) => SortVectorsCounterClockwise(a, b, minYPos));
            foreach (var p in points)
            {
                OZDebug.DrawXYCircle(p,.1f,Color.green,0);
            }
            OZDebug.DrawXYCircle(minYPos,.1f,Color.red,0);

            vectorStack.Clear();
            vectorStack.Push(points[0]);
            vectorStack.Push(points[1]);

            for (int i = 2; i < n; i++)
            {
                Vector2 next = points[i];
                Vector2 p = vectorStack.Pop();
                
                while (vectorStack.Count > 1 && IsClockwise(vectorStack.Peek(),p, next) <=0)
                {
                    p = vectorStack.Pop();
                }
                
                vectorStack.Push(p);
                vectorStack.Push(next);
            }

            Vector2 prev = vectorStack.Pop();
            if (IsClockwise(vectorStack.Peek(), prev, minYPos) > 0)
            {
                vectorStack.Push(prev);
            }
            
            Vector2[] hullPoints = new Vector2[vectorStack.Count];
            int c = 0;
            while (vectorStack.Count != 0)
            {
                hullPoints[c++] = vectorStack.Pop();
            }

            return hullPoints;
        }
    }
}