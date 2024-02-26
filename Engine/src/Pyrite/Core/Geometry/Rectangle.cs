namespace Pyrite.Core.Geometry
{
    public struct Rectangle
    {
        public float X;
        public float Y;
        public float Width;
        public float Height;

        public float Left { readonly get => X; set => X = value; }
        public float Right { readonly get => X + Width; set => Width = X - value; }
        public float Top { readonly get => Y; set => Y = value; }
        public float Bottom { readonly get => Y + Height; set => Height = Y - value; }

        public Vector2 Size
        {
            readonly get => new(Width, Height);
            set { Width = value.X; Height = value.Y; }
        }

        public readonly Vector2 TopLeft => new(X, Y);
        public readonly Vector2 TopCenter => new(X + (Width / 2f), Y);
        public readonly Vector2 TopRight => new(X + Width, Y);
        public readonly Vector2 BottomRight => new(X + Width, Y + Height);
        public readonly Vector2 BottomCenter => new(X + (Width / 2f), Y + Height);
        public readonly Vector2 BottomLeft => new(X, Y + Height);
        public readonly Vector2 CenterLeft => new(X, Y + (Height / 2f));
        public readonly Vector2 Center => new(X + (Width / 2f), Y + (Height / 2f));

        public Rectangle(Vector2 position, Vector2 size)
        {
            X = position.X;
            Y = position.Y;
            Width = size.X;
            Height = size.Y;
        }

        public Rectangle(Point position, Point size)
        {
            X = position.X;
            Y = position.Y;
            Width = size.X;
            Height = size.Y;
        }
        public Rectangle(float x, float y, float width, float height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        public Rectangle(int x, int y, int width, int height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        public static Rectangle Pixel => new(0, 0, 1, 1);

        public static Rectangle FromCoordinates(float top, float bottom, float left, float right)
        {
            return new Rectangle(
                left,
                top,
                right - left,
                bottom - top);
        }

        public static Rectangle FromCoordinates(Vector2 topLeft, Vector2 bottomRight) =>
            FromCoordinates(top: topLeft.Y, bottom: bottomRight.Y, left: topLeft.X, right: bottomRight.X);

        public readonly Rectangle AddPosition(Vector2 position)
            => new(X + position.X, Y + position.Y, Width, Height);
        public readonly Rectangle AddPosition(Point position)
            => new(X + position.X, Y + position.Y, Width, Height);
        public readonly Rectangle SetPosition(Vector2 position)
            => new(position.X, position.Y, Width, Height);
        public readonly Rectangle Expand(int value)
            => new(X - value, Y - value, Width + value * 2f, Height + value * 2f);
        public readonly Rectangle Expand(float value)
            => new(X - value, Y - value, Width + value * 2f, Height + value * 2f);

        public readonly Rectangle AddPadding(float left, float top, float right, float bottom) 
            => new(X - left, Y - top, Width + left + right, Height + top + bottom);
        public static Rectangle FromIntersection(Rectangle a, Rectangle b)
        {
            return FromAbsolute(
                Math.Max(a.Left, b.Left),
                Math.Min(a.Right, b.Right),
                Math.Max(a.Top, b.Top),
                Math.Min(a.Bottom, b.Bottom));
        }

        private static Rectangle FromAbsolute(float left, float right, float top, float bottom)
        {
            return new(left, top, right - left, bottom - top);
        }

        public readonly bool Touches(Rectangle other)
        {
            return other.Left <= Right &&
                   Left <= other.Right &&
                   other.Top <= Bottom &&
                   Top <= other.Bottom;
        }

        public readonly bool TouchesInside(Rectangle other)
        {
            return other.Left < Right &&
                   Left < other.Right &&
                   other.Top < Bottom &&
                   Top < other.Bottom;
        }
        public readonly bool Contains(Vector2 vector) => Contains(vector.X, vector.Y);
        public readonly bool Contains(int X, int Y) => Contains((float)X, (float)Y);

        public readonly bool Contains(Point point) => Contains(point.X, point.Y);
        public readonly bool Contains(float X, float Y)
        {
            return X > Left && X < Right && Y > Top && Y < Bottom;
        }


        public static bool operator ==(Rectangle a, Rectangle b)
            => a.X == b.X
            && a.Y == b.Y
            && a.Width == b.Width
            && a.Height == b.Height;

        public static bool operator !=(Rectangle a, Rectangle b)
            => !(a == b);

        public static Rectangle operator *(Rectangle r, float v)
            => new(r.X * v, r.Y * v, r.Width * v, r.Height * v);
        public static Rectangle operator +(Rectangle r, Vector2 u)
            => new(r.X + u.X, r.Y + u.Y, r.Width, r.Height);
        public static Rectangle operator -(Rectangle r, Vector2 u)
            => new(r.X - u.X, r.Y - u.Y, r.Width, r.Height);

        public readonly bool Equals(Rectangle other)
            => X == other.X 
            && Y == other.Y 
            && Width == other.Width 
            && Height == other.Height;

        public override readonly bool Equals(object? obj)
        {
            if (obj is Rectangle rect)
                return this == rect;
            return false;
        }

        public override readonly int GetHashCode() 
            => HashCode.Combine(X, Y, Width, Height);

    }
}
