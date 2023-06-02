using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using CollisionDetection2D.BoundingVolumes;
using UnityEngine;

public class AABBMemoryTestMono : MonoBehaviour
{
    void Start()
    {
        AABBMinMax aabbMinMax = new AABBMinMax();
        AABBMinWidth aabbMinWidth = new AABBMinWidth();
        AABBCenterRadius aabbCenterRadius = new AABBCenterRadius();
        
        Debug.Log("AABBMinMax: " + Marshal.SizeOf(aabbMinMax));
        Debug.Log("AABBMinWidth: " + Marshal.SizeOf(aabbMinWidth));
        Debug.Log("AABBCenterRadius: " + Marshal.SizeOf(aabbCenterRadius));
    }

    void Update()
    {
        
    }
}
