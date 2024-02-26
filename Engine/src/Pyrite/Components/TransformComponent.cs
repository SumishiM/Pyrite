using Ignite.Components;
using Pyrite.Core;
using Pyrite.Core.Geometry;

namespace Pyrite.Components
{
    public readonly struct TransformComponent : IComponent
    {
        public Transform Transform { get; init; }

        /// <summary>
        /// Transform position
        /// </summary>
        public readonly Vector2 Position { get => Transform.Position; set => Transform.Position = value; }

        /// <summary>
        /// Transfrom rotation as a degres angle
        /// </summary>
        public readonly float Rotation { get => Transform.Rotation; set => Transform.Rotation = value; }

        /// <summary>
        /// Transform scale
        /// </summary>
        public readonly Vector2 Scale { get => Transform.Scale; set => Transform.Scale = value; }

        public TransformComponent()
        {
            Transform = Transform.Empty;
        }

        public TransformComponent(Transform? transform = null)
        {
            Transform = transform ?? Transform.Empty;
        }   

        public static implicit operator TransformComponent(Transform transform) => new (transform);
        public static implicit operator Transform(TransformComponent transform) => transform.Transform;
    }
}
