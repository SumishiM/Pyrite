using Pyrite.Core;
using Pyrite.Physics.Colliders;

namespace Pyrite.Physics
{
    public abstract class PhysicActor : Component
    {
        protected float _xRemainder;
        protected float _yRemainder;

        public Collider? Collider { get; private set; }
    }
}
