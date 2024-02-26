using Ignite.Components;
using Pyrite.Core.Geometry;

namespace Pyrite.Components
{
    public struct VelocityComponent : IComponent
    {
        /// <summary>
        /// Velocity Vector2 
        /// </summary>
        public Vector2 Velocity { get; set; }

        /// <summary>
        /// Empty <see cref="VelocityComponent"/> with <see cref="Velocity"/> set to [0f, 0f]
        /// </summary>
        public VelocityComponent() => Velocity = Vector2.Zero;

        /// <summary>
        /// <see cref="VelocityComponent"/> component from <see cref="Vector2"/>
        /// </summary>
        public VelocityComponent(Vector2 velocity) => Velocity = velocity;

        public static implicit operator VelocityComponent(Vector2 vector) => new(vector);
        public static implicit operator Vector2(VelocityComponent velocity) => velocity.Velocity;
    }
}
