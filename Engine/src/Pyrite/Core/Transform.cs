

using Pyrite.Core.Geometry;

namespace Pyrite.Core
{
    public class Transform
    {
        public Vector2 Position { get; set; } = Vector2.Zero;

        public Vector2 Scale { get; set; } = Vector2.One;

        public float Rotation { get; set; } = 0f;

        public Transform() { }

        public Transform(Vector2 position)
        {
            Position = position;
        }

        public Transform(Vector2 position, Vector2 scale)
        {
            Position = position;
            Scale = scale;
        }

        public Transform(Vector2 position, Vector2 scale, float rotation)
        {
            Position = position;
            Scale = scale;
            Rotation = rotation;
        }

        public static Transform Empty => new();
    }
}
