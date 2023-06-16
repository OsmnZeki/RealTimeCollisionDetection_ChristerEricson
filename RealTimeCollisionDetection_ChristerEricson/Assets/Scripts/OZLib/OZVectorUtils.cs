using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OZLib
{
    public static class OZVectorUtils
    {
        public static void Vector3ArrayToVector2Array(Vector3[] src, Vector2[] dst)
        {
            if(dst.Length != src.Length)
            {
                Debug.LogError("dst.Length != src.Length");
                return;
            }
            
            for (int i = 0; i < src.Length; i++)
            {
                dst[i] = src[i];
            }
        }
        
    }
}


