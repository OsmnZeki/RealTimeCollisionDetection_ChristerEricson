using UnityEngine;

namespace OZLib
{
    public static class OZDebug
    {
        public static void DrawXYCircle(Vector2 center, float radius, Color color, float duration, int nSteps = 30)
        {
            float anglePerStep = 360f / nSteps;
            for (int i = 0; i < nSteps; i++)
            {
                Quaternion rot1 = Quaternion.AngleAxis(anglePerStep * i, Vector3.forward);
                Quaternion rot2 = Quaternion.AngleAxis(anglePerStep * (i + 1), Vector3.forward);

                Vector2 p1 = rot1 * Vector2.up * radius;
                Vector2 p2 = rot2 * Vector2.up * radius;

                Debug.DrawLine(center + p1, center + p2, color, duration);
            }
        }

        public static void DrawBox(Vector2 center, Vector2 extents, Color color, float duration)
        {
            Vector2 p1 = center + new Vector2(-extents.x, -extents.y);
            Vector2 p2 = center + new Vector2(extents.x, -extents.y);
            Vector2 p3 = center + new Vector2(extents.x, extents.y);
            Vector2 p4 = center + new Vector2(-extents.x, extents.y);

            Debug.DrawLine(p1, p2, color, duration);
            Debug.DrawLine(p2, p3, color, duration);
            Debug.DrawLine(p3, p4, color, duration);
            Debug.DrawLine(p4, p1, color, duration);
        }

        public static void DrawBoxMinMaxPoints(Vector2 min, Vector2 max, Color color, float duration)
        {
            Vector2 p1 = new Vector2(min.x, min.y);
            Vector2 p2 = new Vector2(max.x, min.y);
            Vector2 p3 = new Vector2(max.x, max.y);
            Vector2 p4 = new Vector2(min.x, max.y);

            Debug.DrawLine(p1, p2, color, duration);
            Debug.DrawLine(p2, p3, color, duration);
            Debug.DrawLine(p3, p4, color, duration);
            Debug.DrawLine(p4, p1, color, duration);
        }

        public static void DrawPath(Vector2[] path, Color color, float duration)
        {
            for (int i = 0; i < path.Length; i++)
            {
                Debug.DrawLine(path[i], path[(i + 1)%path.Length], color, duration);
            }
        }
    }

}