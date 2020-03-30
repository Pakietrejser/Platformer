using UnityEngine;

namespace Utilities
{
    public static class BMath
    {
        public static Vector3 Smoothstep(Vector3 from, Vector3 to, float t)
        {
            Mathf.Clamp01(t);
            if ((from - to).magnitude <= t)
                return to; 
            t = t * t * (3 - 2 * t);
            Vector3 result;
            result.x = from.x + (to.x - from.x) * t;
            result.y = from.y + (to.y - from.y) * t;
            result.z = from.z + (to.z - from.z) * t;
            return result;
        }
        
        public static Vector3 Smoothstep2D(Vector2 from, Vector2 to, float t)
        {
            Mathf.Clamp01(t);
            if ((from - to).magnitude <= t)
                return to; 
            t = t * t * (3 - 2 * t);
            Vector2 result;
            result.x = from.x + (to.x - from.x) * t;
            result.y = from.y + (to.y - from.y) * t;
            return result;
        }
        
        public static float Smoothstep1D(float from, float to, float t)
        {
            Mathf.Clamp01(t);
            if (Mathf.Abs(from - to) <= t)
                return to; 
            t = t * t * (3 - 2 * t);
            return from + (to - from) * t;;
        }
    }
}

