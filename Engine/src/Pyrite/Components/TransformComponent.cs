using Pyrite.Core;
using Pyrite.Core.Geometry;

namespace Pyrite.Components
{
    public class TransformComponent : Component
    {
        public Transform Transform { get; set; }

        public Vector2 Position { get => Transform.Position; set => Transform.Position = value; }
        public float Rotation { get => Transform.Rotation; set => Transform.Rotation = value; }
        public Vector2 Scale { get => Transform.Scale; set => Transform.Scale = value; }

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
