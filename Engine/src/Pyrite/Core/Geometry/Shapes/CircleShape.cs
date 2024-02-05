using Pyrite.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pyrite.Core.Geometry.Shapes
{
    public struct CircleShape : IShape
    {
        public readonly float Radius;

        public readonly Point Offset;

        public Circle Circle => new(Offset.X, Offset.Y, Radius);

        public CircleShape(float radius, Point offset) => (Radius, Offset) = (radius, offset);

        public Rectangle ToRectangle()
        {
            int radius = Calculator.RoundToInt(Radius);
            int diameter = Calculator.RoundToInt(Radius * 2);
            return new Rectangle(Offset.X - radius, Offset.Y - radius, diameter, diameter);
        }

        public Circle ToInnerCircle() 
            => Circle;

        public Circle ToOuterCircle() 
            => Circle;
    }
}
