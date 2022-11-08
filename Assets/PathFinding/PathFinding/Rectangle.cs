using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;

[Serializable]
public struct Rectangle
{
    [SerializeField, FormerlySerializedAs("min")] private Vector2 _min;
    [SerializeField, FormerlySerializedAs("max")] private Vector2 _max;
    public Vector2 max
    {
        get => math.max(_min, _max);
        set => _max = value;
    }
    public Vector2 min
    {
        get => math.min(_min, _max);
        set => _min = value;
    }
    public Rectangle(Vector2 a, Vector2 b)
    {
        _min = math.min(a, b);
        _max = math.max(a, b);;
    }

    public static bool operator ==(Rectangle lhs, Rectangle rhs) => lhs.max == rhs.max && lhs.min == rhs.min;
    public static bool operator !=(Rectangle lhs, Rectangle rhs) => lhs.max != rhs.max || lhs.min != rhs.min;
    
    public bool Equals(Rectangle obj) => obj.max == max && obj.min == min;

    public override string ToString() => "Rect : Min : " + min + ", Max : " + max;
    
}
