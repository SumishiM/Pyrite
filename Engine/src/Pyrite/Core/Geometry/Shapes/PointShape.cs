namespace Pyrite.Core.Geometry.Shapes
{
    public readonly struct PointShape : IShape
    {
        public readonly Point Point = Point.Zero;

        public PointShape(Point point)
        {
            Point = point;
        }

        public readonly Circle ToInnerCircle()
            => new(Point, 1f);

        public readonly Circle ToOuterCircle()
            => new(Point, 1f);

        public readonly Rectangle ToRectangle() 
            => new(Point, Point.One);
    }
}
