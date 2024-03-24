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

        /// <summary>
        /// Check whether a <see cref="Vector2"/> is in a <see cref="Rectangle"/>
        /// </summary>
        /// <param name="v"><see cref="Vector2"/> to check</param>
        /// <param name="r"><see cref="Rectangle"/> to check</param>
        /// <returns></returns>
        public static bool InRectangle(Vector2 v, Rectangle r)
            => r.Contains(v);
            
        /// <summary>
        /// Check whether a <see cref="Point"/> is in a <see cref="Rectangle"/>
        /// </summary>
        /// <param name="v"><see cref="Point"/> to check</param>
        /// <param name="r"><see cref="Rectangle"/> to check</param>
        /// <returns></returns>
        public static bool InRectangle(Point p, Rectangle r)
            => r.Contains(p);

        /// <summary>
        /// Check whether a couple of coordonate are in a rectangle 
        /// </summary>
        /// <returns></returns>
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
