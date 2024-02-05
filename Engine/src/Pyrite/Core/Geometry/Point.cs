using Pyrite.Utils;

namespace Pyrite.Core.Geometry
{
    public struct Point
    {
        public int X;
        public int Y;

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public Point(float x, float y)
        {
            X = Calculator.RoundToInt(x);
            Y = Calculator.RoundToInt(y);
        }

        public Point (Vector2 v) : this(v.X, v.Y) { }

        #region Constants
        public static Point Zero => new(0, 0);
        public static Point One => new(1, 1);
        public static Point UnitX => new(1, 0);
        public static Point UnitY => new(0, 1);
        #endregion

        #region Operators
        public static implicit operator Vector2(Point p) => new(p.X, p.Y);

        public static Point operator +(Point a, Point b)
            => new(a.X + b.X, a.Y + b.Y);
        public static Point operator +(Point a, Vector2 b)
            => new(a.X + Calculator.RoundToInt(b.X), a.Y + Calculator.RoundToInt(b.Y));
        
        public static Point operator -(Point a, Point b)
            => new(a.X - b.X, a.Y - b.Y);
        public static Point operator -(Point a, Vector2 b)
            => new(a.X - Calculator.RoundToInt(b.X), a.Y - Calculator.RoundToInt(b.Y));

        public static Point operator *(Point p, int s)
            => new(p.X * s, p.Y * s);
        public static Point operator *(Point p, float s)
            => new(Calculator.RoundToInt(p.X * s), Calculator.RoundToInt(p.Y * s));
        public static Point operator *(Point a, Point b)
            => new(Calculator.RoundToInt(a.X * b.X), Calculator.RoundToInt(a.Y * b.Y));
        
        public static Point operator /(Point a, Point b)
            => new(a.X / b.X, a.Y / b.Y);
        public static Point operator /(Point p, int s)
            => new(p.X / s, p.Y / s);
        public static Point operator /(Point p, float s)
            => new(Calculator.RoundToInt(p.X / s), Calculator.RoundToInt(p.Y / s));
        #endregion
    }
}
