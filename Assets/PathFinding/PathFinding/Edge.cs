using System;
using Unity.Mathematics;
using UnityEngine;

[Serializable]
public struct Edge
{
    public Rectangle First;
    public Rectangle Second;
    
    public Vector3 Start;
    public Vector3 Center;
    public Vector3 End;
    
    public Edge(Rectangle first, Rectangle second, Vector3 start, Vector3 end)
    {
        First = first;
        Second = second;
        Start = start;
        End = end;
        Center = math.lerp(start, end, 0.5f);
    }
    
    /// <summary>
    /// / Only Editor, Gizmos
    /// </summary>
    public void UpdateCenter() => Center = math.lerp(Start, End, 0.5f);
    
}
