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

        public static Point Zero
        {
            get => new(0, 0);
        }

        public static Point One
        {
            get => new(1, 1);
        }
        
        public static Point UnitX
        {
            get => new(1, 0);
        }

        public static Point UnitY
        {
            get => new(0, 1);
        }

        public static implicit operator Point(Vector2 v) => new((int)v.X, (int)v.Y);
        public static implicit operator Vector2(Point p) => new(p.X, p.Y);

        public static Point operator +(Point a, Point b)
        {
            return new Point(a.X + b.X, a.Y + b.Y);
        }

        public static Point operator -(Point a, Point b)
        {
            return new Point(a.X - b.X, a.Y - b.Y);
        }
        public static Point operator *(Point a, Point b)
        {
            return new Point(a.X * b.X, a.Y * b.Y);
        }

        public static Point operator /(Point a, Point b)
        {
            return new Point(a.X / b.X, a.Y / b.Y);
        }

        public static Point operator *(Point p, int s)
        {
            return new Point(p.X * s, p.Y * s);
        }

        public static Point operator /(Point p, int s)
        {
            return new Point(p.X / s, p.Y / s);
        }
    }
}
