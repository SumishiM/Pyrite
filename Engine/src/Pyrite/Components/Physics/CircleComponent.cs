using Ignite.Attributes;
using Ignite.Components;
using Pyrite.Core.Geometry;
using Pyrite.Core.Geometry.Shapes;

namespace Pyrite.Components.Physics
{
    [RequireComponent(typeof(TransformComponent))]
    public class BoxComponent : IComponent
    {
        public Point Size;
        public Point Offset;
        public Vector2 Origin;

        public BoxComponent()
        {
            Size = new Point(16, 16);
            Offset = Point.Zero;
            Origin = Size / 2f;
        }

        public BoxComponent(Point size, Point offset)
        {
            Size = size;
            Offset = offset;
            Origin = Size / 2f;
        }

        public BoxComponent(Vector2 size, Vector2 offset)
        {
            Size = size;
            Offset = offset;
            Origin = Size / 2f;
        }

        public BoxComponent(ref BoxShape box)
        {
            Size = box.Size;
            Offset = box.Offset;
        }

        public BoxShape Shape => this;

        
        public static implicit operator BoxComponent(BoxShape box) => new(ref box);
        public static implicit operator BoxShape(BoxComponent box) => new(box.Size, box.Offset);
    }
}
