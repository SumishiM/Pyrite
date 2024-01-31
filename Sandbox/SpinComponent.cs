using Ignite.Attributes;
using Ignite.Components;
using Pyrite.Components;

namespace Sandbox
{
    [RequireComponent(typeof(TransformComponent))]
    public class SpinComponent : IComponent
    {
        public float SpinSpeed = 360f;
    }
}
