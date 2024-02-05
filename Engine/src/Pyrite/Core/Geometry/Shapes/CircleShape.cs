using Pyrite.Utils;
namespace Pyrite.Core.Geometry.Shapes
{
    public readonly struct CircleShape : IShape
    {
        public readonly float Radius;

        public readonly Point Offset;

        public readonly Circle Circle => new(Offset.X, Offset.Y, Radius);

        public CircleShape(float radius, Point offset) => (Radius, Offset) = (radius, offset);

        public readonly Rectangle ToRectangle()
        {
            int radius = Calculator.RoundToInt(Radius);
            int diameter = Calculator.RoundToInt(Radius * 2);
            return new Rectangle(Offset.X - radius, Offset.Y - radius, diameter, diameter);
        }

        public readonly Circle ToInnerCircle() 
            => Circle;

        public readonly Circle ToOuterCircle() 
            => Circle;
    }
}
