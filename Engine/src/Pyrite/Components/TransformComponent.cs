using Ignite.Components;
using Pyrite.Core;

namespace Pyrite.Components
{
    public class TransformComponent : IComponent
    {
        public Transform Transform { get; set; }

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
