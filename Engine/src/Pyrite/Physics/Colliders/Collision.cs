using System.Runtime.InteropServices;

namespace Pyrite.Physics.Colliders
{
    public static class Collision
    {
        public static bool ColliderToCollider(Collider a, Collider b)
        {
            // native to xna
            // may change if i start making all myself
            return a.Bounds.Intersects(b.Bounds);
        }

        public static bool RectangleToCircle(RectangleCollider rectangle, CircleCollider circle)
        {
            return false;
        }

        public static bool RectangleToPolygon(RectangleCollider rectangle, PolygonCollider polygon)
        {
            return false;
        }

        public static bool CircleToPolygon(CircleCollider circle, PolygonCollider polygon)
        {
            return false;
        }
    }
}
