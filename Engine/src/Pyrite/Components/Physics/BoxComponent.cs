using Ignite.Attributes;
using Ignite.Components;
using Pyrite.Core.Geometry;
using Pyrite.Core.Geometry.Shapes;

namespace Pyrite.Components.Physics
{
    [RequireComponent(typeof(TransformComponent))]
    public class CircleComponent : IComponent
    {
        public float Radius;
        public Point Offset;

        public CircleComponent()
        {
            Radius = 8f;
            Offset = Point.Zero;
        }

        public CircleComponent(Point offset, float radius)
        {
            Radius = radius;
            Offset = offset;
        }

        public CircleComponent(Vector2 offset, float radius)
        {
            Radius = radius;
            Offset = offset;
        }

        public CircleComponent(ref CircleShape circle)
        {
            Radius = circle.Radius;
            Offset = circle.Offset;
        }

        public CircleShape Shape => this;

        
        public static implicit operator CircleComponent(CircleShape box) => new(ref box);
        public static implicit operator CircleShape(CircleComponent box) => new(box.Radius, box.Offset);
    }
}
