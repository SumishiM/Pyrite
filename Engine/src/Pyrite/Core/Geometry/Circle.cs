using Pyrite.Utils;

namespace Pyrite.Core.Geometry
{
    public struct Circle
    {
        /// <summary>
        /// World X position of the circle
        /// </summary>
        public float X;
        
        /// <summary>
        /// World Y position of the circle
        /// </summary>
        public float Y;

        /// <summary>
        /// Radius of the circle
        /// </summary>
        public float Radius;
        
        /// <summary>
        /// Center of the circle
        /// </summary>
        public readonly Vector2 Center => new(X, Y);
        
        /// <summary>
        /// Create a circle of <paramref name="radius"/> at position [0, 0]
        /// </summary>
        public Circle(float radius)
        {
            X = 0;
            Y = 0;
            Radius = radius;
        }

        /// <summary>
        /// Create a circle of <paramref name="radius"/> at position [x, y]
        /// </summary>
        public Circle(float x, float y, float radius)
        {
            X = x;
            Y = y;
            Radius = radius;
        }

        
        /// <summary>
        /// Create a circle of <paramref name="radius"/> at given position
        /// </summary>
        public Circle(Vector2 center, float radius) : this(center.X, center.Y, radius) { }

        /// <summary>
        /// Add a given position to the circle current position
        /// </summary>
        /// <returns>A new circle with updated position</returns>
        public Circle AddPosition(Point position) => new(X + position.X, Y + position.Y, Radius);
        
        /// <summary>
        /// Add a given position to the circle current position
        /// </summary>
        /// <returns>A new circle with updated position</returns>
        public Circle AddPosition(Vector2 position) => new(X + position.X, Y + position.Y, Radius);

        /// <summary>
        /// Wheck whether the circle contains a point
        /// </summary>
        public bool Contains(Vector2 vector2) => (new Vector2(X, Y) - vector2).SquaredLength() < Radius * Radius;

        /// <summary>
        /// Wheck whether the circle contains a point
        /// </summary>
        public bool Contains(Point point) => (new Vector2(X, Y) - point).SquaredLength() < Radius * Radius;

        //internal IEnumerable<Vector2> MakePolygon()
        //{
        //    foreach (Vector2 point in Geometry.CreateCircle(Radius, 12))
        //    {
        //        yield return point + new Vector2(X, Y);
        //    }
        //}
    }
}
