using Ignite.Attributes;
using Ignite.Components;
using Pyrite.Components;

namespace Sandbox
{
    [RequireComponent(typeof(TransformComponent))]
    public struct SpinComponent : IComponent
    {
        public float SpinSpeed = 360f;

        public SpinComponent() { }
        public SpinComponent(float speed)
        {
            SpinSpeed = speed;
        }
    }
}
