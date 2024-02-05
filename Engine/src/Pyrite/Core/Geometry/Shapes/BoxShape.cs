using Pyrite.Utils;
namespace Pyrite.Core.Geometry.Shapes
{
    public readonly struct BoxShape : IShape
    {
        public readonly int Width = 16;
        public readonly int Height = 16;

        public readonly Vector2 Origin = Vector2.Zero;
        public readonly Point Offset = Point.One * 16;

        public readonly Point Size => new(Width, Height);
        /// <summary>
        /// Simple shape getter
        /// </summary>
        public readonly Rectangle Rectangle => new(
            -Calculator.RoundToInt(Width * Origin.X) + Offset.X, 
            -Calculator.RoundToInt(Height * Origin.Y) + Offset.Y, 
            Width, 
            Height);

        public BoxShape() { }

        public BoxShape(Rectangle rectangle)
        {
            Origin = Vector2.Zero;
            Offset = rectangle.TopLeft;
            Width = (int)rectangle.Width;
            Height = (int)rectangle.Height;
        }

        public BoxShape(Vector2 origin, Point offset, int width, int height)
        {
            Origin = origin;
            Offset = offset;
            Width = width;
            Height = height;
        }

        public BoxShape ResizeTopRight(Vector2 newTopRight)
        {
            Vector2 delta = Offset - newTopRight;
            return new(
                Origin,
                newTopRight,
                Width + Calculator.RoundToInt(delta.X),
                Height + Calculator.RoundToInt(delta.Y)
                );
        }
        public BoxShape ResizeBottomRight(Vector2 newBottomLeft)
        {
            Point origin = ((Vector2.One - Origin) * Size);
            Vector2 delta = Offset + origin - newBottomLeft;
            return new(
                Origin,
                Offset,
                Width - (int)delta.X,
                Height - (int)delta.Y
                );
        }

        public readonly Rectangle ToRectangle() 
            => Rectangle;

        public readonly Circle ToInnerCircle()
            => new(Rectangle.Center, Calculator.Min(Width, Height) * 0.5f);

        public readonly Circle ToOuterCircle()
            => new(Rectangle.Center, new Vector2(Width, Height).Length() * 0.5f);
    }
}
