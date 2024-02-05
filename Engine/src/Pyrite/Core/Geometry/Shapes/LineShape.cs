namespace Pyrite.Core.Geometry.Shapes
{
    public readonly struct LineShape : IShape
    {
        public readonly Point Start = Point.Zero;
        public readonly Point End = Point.Zero;

        public LineShape(Point start, Point end)
        {
            Start = start;
            End = end;
        }

        public Line Line => new(Start, End);

        public readonly Rectangle ToRectangle()
        {
            int left = Math.Min(Start.X, End.X);
            int right = Math.Max(Start.X, End.X);
            int top = Math.Min(Start.Y, End.Y);
            int bottom = Math.Max(Start.Y, End.Y);

            return new(left, top, right - left, bottom - top);
        }

        public readonly Circle ToInnerCircle()
            => new(Line.Center, Vector2.Distance(Start, End) * 0.5f);

        public readonly Circle ToOuterCircle()
            => ToInnerCircle();

    }
}
