using Pyrite.Utils;
using Silk.NET.Maths;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pyrite.Core.Geometry
{
    public struct Line
    {
        public float X1;
        public float Y1; 
        public float X2; 
        public float Y2;

        public readonly Vector2 Start => new(X1, Y1);
        public readonly Vector2 End => new(X2, Y2);

        public readonly float Top => MathF.Max(Y1, Y2);
        public readonly float Bottom => MathF.Min(Y1, Y2);
        public readonly float Left => MathF.Min(X1, X2);
        public readonly float Right => MathF.Max(X1, X2);

        public readonly float Width => Right - Left;
        public readonly float Height => Top - Bottom;

        public readonly Vector2 Center => new(Left + Width / 2f, Top + Height / 2f);


        public Line(float x1, float y1, float x2, float y2)
        {
            X1 = x1;
            Y1 = y1;
            X2 = x2;
            Y2 = y2;
        }

        public Line(Vector2 start, Vector2 end)
        {
            X1 = start.X;
            Y1 = start.Y;
            X2 = end.X;
            Y2 = end.Y;
        }

        public readonly bool HasPoint(Vector2 point, float precision = float.Epsilon)
        {
            float d1 = (point - Start).Length();
            float d2 = (point - End).Length();
            float len = Length();
            float sum = d1 + d2;

            return sum >= len - precision && sum <= len + precision;
        }

        public readonly float Length()
            => (Start - End).Length();

        public readonly float SquaredLength()
            => (Start - End).SquaredLength();

        /// <summary>
        /// Intersection test on another line. (http://ideone.com/PnPJgb)
        /// </summary>
        /// <param name="other">The line to test against</param>
        /// <returns></returns>
        public readonly bool Intersects(Line other)
        {
            Vector2 A = Start;
            Vector2 B = End;
            Vector2 C = other.Start;
            Vector2 D = other.End;

            Vector2 CmP = new(C.X - A.X, C.Y - A.Y);
            Vector2 r = new(B.X - A.X, B.Y - A.Y);
            Vector2 s = new(D.X - C.X, D.Y - C.Y);

            float CmPxr = CmP.X * r.Y - CmP.Y * r.X;
            float CmPxs = CmP.X * s.Y - CmP.Y * s.X;
            float rxs = r.X * s.Y - r.Y * s.X;

            if (CmPxr == 0f)
            {
                // Lines are collinear, and so intersect if they have any overlap

                return ((C.X - A.X < 0f) != (C.X - B.X < 0f))
                    || ((C.Y - A.Y < 0f) != (C.Y - B.Y < 0f));
            }

            if (rxs == 0f)
                return false; // Lines are parallel.

            float rxsr = 1f / rxs;
            float t = CmPxs * rxsr;
            float u = CmPxr * rxsr;

            return (t >= 0f) && (t <= 1f) && (u >= 0f) && (u <= 1f);
        }

        public bool TryGetIntersectingPoint(Line other, out Vector2 hitPoint)
        {
            return TryGetIntersectingPoint(this, other, out hitPoint);
        }

        public static bool TryGetIntersectingPoint(Line line1, Line line2, out Vector2 hitPoint)
        {
            Vector2 a = line1.End - line1.Start;
            Vector2 b = line2.Start - line2.End;
            Vector2 c = line1.Start - line2.Start;

            float alphaNumerator = b.Y * c.X - b.X * c.Y;
            float betaNumerator = a.X * c.Y - a.Y * c.X;
            float denominator = a.Y * b.X - a.X * b.Y;

            if (denominator.IsAlmostZero())
            {
                hitPoint = default;
                return false;
            }
            else if (denominator > 0)
            {
                if (alphaNumerator < 0 || alphaNumerator > denominator || betaNumerator < 0 || betaNumerator > denominator)
                {
                    hitPoint = default;
                    return false;
                }
            }
            else if (alphaNumerator > 0 || alphaNumerator < denominator || betaNumerator > 0 || betaNumerator < denominator)
            {
                hitPoint = default;
                return false;
            }
            // If lines intersect, then the intersection point can be found
            float px = (line1.X1 * line1.Y2 - line1.Y1 * line1.X2) * (line2.X1 - line2.X2) - (line1.X1 - line1.X2) * (line2.X1 * line2.Y2 - line2.Y1 * line2.X2);
            float py = (line1.X1 * line1.Y2 - line1.Y1 * line1.X2) * (line2.Y1 - line2.Y2) - (line1.Y1 - line1.Y2) * (line2.X1 * line2.Y2 - line2.Y1 * line2.X2);

            hitPoint = new Vector2(px, py) / denominator;
            return true;
        }

        /// <summary>
        /// Check intersection against a rectangle.
        /// </summary>
        /// <returns>True if the line intersects any line on the rectangle, or if the line is inside the rectangle.</returns>
        public bool IntersectsRect(Rectangle rectangle)
            => IntersectsRect(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);

        /// <summary>
        /// Check intersection against a rectangle.
        /// </summary>
        /// <param name="x">X Position of the rectangle.</param>
        /// <param name="y">Y Position of the rectangle.</param>
        /// <param name="width">Width of the rectangle.</param>
        /// <param name="height">Height of the rectangle.</param>
        /// <returns>True if the line intersects any line on the rectangle, or if the line is inside the rectangle.</returns>
        public bool IntersectsRect(float x, float y, float width, float height)
        {
            if (Geometry.InRectangle(X1, Y1, x, y, width, height)) return true;
            if (Geometry.InRectangle(X2, Y2, x, y, width, height)) return true;
            if (Intersects(new Line(x, y, x + width, y))) return true;
            if (Intersects(new Line(x + width, y, x + width, y + height))) return true;
            if (Intersects(new Line(x + width, y + height, x, y + height))) return true;
            if (Intersects(new Line(x, y + height, x, y))) return true;

            return false;
        }

        /// <summary>
        /// Check the intersection against a circle.
        /// </summary>
        /// <param name="circle"></param>
        /// <param name="radius"></param>
        /// <returns></returns>
        public bool IntersectCircle(Vector2 circle, float radius)
        {
            // find the closest point on the line segment to the center of the circle
            Vector2 line = End - Start;
            float lineLength = line.Length();
            Vector2 lineNorm = line * (1 / lineLength);
            Vector2 segmentToCircle = circle - Start;
            float closestPointOnSegment = Vector2.Dot(segmentToCircle, line) / lineLength;

            // Special cases where the closest point happens to be the end points
            Vector2 closest;
            if (closestPointOnSegment < 0) closest = Start;
            else if (closestPointOnSegment > lineLength) closest = End;
            else closest = Start + closestPointOnSegment * lineNorm;

            // Find that distance.  If it is less than the radius, then we
            // are within the circle
            var distanceFromClosest = circle - closest;
            var distanceFromClosestLength = distanceFromClosest.Length();
            if (distanceFromClosestLength > radius) return false;

            return true;
        }

        public bool IntersectsCircle(Circle circle)
        {
            return IntersectCircle(circle.Center, circle.Radius);
        }
    }
}
