using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pyrite.Core.Geometry
{
    public static class Geometry
    {
        //private static Dictionary<String, Vector2[]> _circleCache = [];

        //public static Vector2[] CreateCircle(double radius, int sides)
        //{
        //    // Look for a cached version of this circle
        //    String circleKey = $"{radius}x{sides}";
        //    if (_circleCache.TryGetValue(circleKey, out Vector2[]? value))
        //    {
        //        return value;
        //    }

        //    List<Vector2> vectors = [];

        //    const double max = 2.0 * Math.PI;
        //    double step = max / sides;

        //    for (double theta = 0.0; theta < max; theta += step)
        //    {
        //        vectors.Add(new Vector2((float)(radius * Math.Cos(theta)), (float)(radius * Math.Sin(theta))));
        //    }

        //    // Cache this circle so that it can be quickly drawn next time
        //    var result = vectors.ToArray();
        //    _circleCache.Add(circleKey, result);

        //    return result;
        //}

        public static bool InRectangle(Vector2 u, Rectangle r)
            => r.Contains(u);

        public static bool InRectangle(float x, float y, float rx, float ry, float rw, float rh)
        {
            if (x <= rx) return false;
            if (x >= rx + rw) return false;
            if (y <= ry) return false;
            if (y >= ry + rh) return false;
            return true;
        }

        public static bool CheckOverlap(float minA, float maxA, float minB, float maxB)
        {
            return minA <= maxB && minB <= maxA;
        }

        public static bool CheckOverlap((float Min, float Max) a, (float Min, float Max) b)
        {
            return a.Min <= b.Max && b.Min <= a.Max;
        }
    }
}
