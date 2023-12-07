using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

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

        public static bool RectangleToCircle(RectangleCollider r, CircleCollider c)
        {
            Vector2 u = r.Center - new Vector2(r.Right, r.Top);
            Vector2 v = new Vector2(MathF.Abs(r.Center.X - c.Center.X), MathF.Abs(r.Center.Y - c.Center.Y));

            return (v - u).LengthSquared() < c.Radius * c.Radius;
        }

        public static bool RectangleToPolygon(RectangleCollider rectangle, PolygonCollider polygon)
        {

            return false;
        }

        public static bool CircleToPolygon(CircleCollider circle, PolygonCollider polygon)
        {
            return false;
        }

        public static bool PolygonToPolygon(PolygonCollider a, PolygonCollider b) { return false; }

        public static bool CircleToCircle(CircleCollider a, CircleCollider b) 
        { 
            return Vector2.DistanceSquared(a.Center, b.Center) < a.Radius * a.Radius + b.Radius * b.Radius;
        }

        public static bool RectangleToRectangle(RectangleCollider a, RectangleCollider b)
        {
            return a.Bounds.Intersects(b.Bounds);
        }

        public static bool Check(
            ICollection<StaticActor> statics, 
            [DisallowNull] DynamicActor dynamicActor, 
            Point movement)
        {
            if( dynamicActor.Collider == null) return false;

            Collider movedCollider = dynamicActor.Collider.PredicateMove(movement);

            foreach (StaticActor solid in statics)
            {
                if( solid.Collider == null)
                    continue;

                if (!Collision.ColliderToCollider(solid.Collider, movedCollider))
                    return false;
            }

            return true;
        }
    }
}
