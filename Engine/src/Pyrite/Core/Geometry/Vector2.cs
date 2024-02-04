namespace Pyrite.Core.Geometry
{
    public struct Vector2
    {
        public float X;
        public float Y;

        public Vector2(float x, float y)
        {
            X = x;
            Y = y;
        }

        public static Vector2 Zero
        {
            get => new(0f, 0f);
        }

        public static Vector2 One
        {
            get => new(1f, 1f);
        }

        public static Vector2 UnitX
        {
            get => new(1f, 0f);
        }

        public static Vector2 UnitY
        {
            get => new(0f, 1f);
        }

        public static implicit operator Point(Vector2 v) => new((int)v.X, (int)v.Y);
        public static implicit operator Vector2(Point p) => new(p.X, p.Y);

        public static Vector2 operator +(Vector2 a, Vector2 b)
        {
            return new Vector2(a.X + b.X, a.Y + b.Y);
        }

        public static Vector2 operator -(Vector2 a, Vector2 b)
        {
            return new Vector2(a.X - b.X, a.Y - b.Y);
        }
        public static Vector2 operator *(Vector2 a, Vector2 b)
        {
            return new Vector2(a.X * b.X, a.Y * b.Y);
        }

        public static Vector2 operator /(Vector2 a, Vector2 b)
        {
            return new Vector2(a.X / b.X, a.Y / b.Y);
        }

        public static Vector2 operator *(Vector2 p, int s)
        {
            return new Vector2(p.X * s, p.Y * s);
        }

        public static Vector2 operator /(Vector2 p, int s)
        {
            return new Vector2(p.X / s, p.Y / s);
        }
    }
}
