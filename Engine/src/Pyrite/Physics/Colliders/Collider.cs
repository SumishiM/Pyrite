using System.Runtime.Serialization;

namespace Pyrite.Physics.Colliders
{
    public abstract class Collider
    {
        public bool Collide { get; set; }

        public int Left => Bounds.Left;
        public int Right => Bounds.Right;
        public int Top => Bounds.Top;
        public int Bottom => Bounds.Bottom;
        public Point Center => Bounds.Center;
        public Point HalfExtends => Bounds.Size / new Point(2, 2);

        public Rectangle Bounds { get; set; }

        public Collider()
        {
        }

    }
}
