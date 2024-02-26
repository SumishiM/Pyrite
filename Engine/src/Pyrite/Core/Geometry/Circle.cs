using Pyrite.Utils;

namespace Pyrite.Core.Geometry
{
    public struct Circle
    {
        public float X;
        public float Y;

        public float Radius;
        public readonly Vector2 Center => new(X, Y);
        
        public Circle(float radius)
        {
            X = 0;
            Y = 0;
            Radius = radius;
        }

        public Circle(float x, float y, float radius)
        {
            X = x;
            Y = y;
            Radius = radius;
        }
        public Circle(Vector2 center, float radius) : this(center.X, center.Y, radius) { }

        public Circle AddPosition(Point position) => new(X + position.X, Y + position.Y, Radius);
        public Circle AddPosition(Vector2 position) => new(X + position.X, Y + position.Y, Radius);

        public bool Contains(Vector2 vector2) => (new Vector2(X, Y) - vector2).SquaredLength() < Radius * Radius;
        public bool Contains(Point point) => (new Vector2(X, Y) - point).SquaredLength() < Radius * Radius;

        //internal IEnumerable<Vector2> MakePolygon()
        //{
        //    foreach (Vector2 point in Geometry.CreateCircle(Radius, 12))
        //    {
        //        yield return point + new Vector2(X, Y);
        //    }
        //}

        public static int EstipulateSidesFromRadius(float radius)
        {
            return Math.Min(22, 6 + Calculator.FloorToInt(radius * 0.45f));
        }
    }
}
