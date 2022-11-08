using UnityEngine;

namespace DefaultNamespace
{
    public static class Extension
    {
        public static bool PointInRect(Rectangle rectangle, Vector2 point)
            => rectangle.max.x >= point.x
            && rectangle.max.y >= point.y
            && rectangle.min.x <= point.x
            && rectangle.min.y <= point.y;
    }
}