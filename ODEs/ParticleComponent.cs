using Ignite.Attributes;
using Ignite.Components;
using Pyrite.Components;

namespace ODEs
{
    [RequireComponent(typeof(TransformComponent))]
    public struct ParticleComponent : IComponent
    {
        public float Speed = 100f;

        public ParticleComponent() { }
        public ParticleComponent(float speed)
        {
            Speed = speed;
        }
    }
}
