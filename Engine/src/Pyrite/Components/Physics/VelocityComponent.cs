using Ignite.Attributes;
using Ignite.Components;
using Pyrite.Core.Geometry;

namespace Pyrite.Components.Physics
{
    [RequireComponent(typeof(TransformComponent))]
    public class VelocityComponent : IComponent
    {
        public Vector2 Velocity { get; set; }

        public VelocityComponent() => Velocity = Vector2.Zero;
        public VelocityComponent(Vector2 velocity) => Velocity = velocity;

        // cast from and to Vector2
        public static implicit operator VelocityComponent(Vector2 vector) => new(vector);
        public static implicit operator Vector2(VelocityComponent velocity) => velocity.Velocity;
    }
}
