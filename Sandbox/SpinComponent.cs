using Ignite;
using Ignite.Attributes;
using Pyrite.Components;

namespace Sandbox
{
    [RequireComponent(typeof(TransformComponent))]
    public class SpinComponent : Component
    {

        public float SpinSpeed = 360f;

    }
}
