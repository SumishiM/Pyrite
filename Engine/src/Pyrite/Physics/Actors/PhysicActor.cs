using Pyrite.Core;
using Pyrite.Physics.Colliders;

namespace Pyrite.Physics
{
    /// <summary>
    /// Base physic object class. 
    /// This component is used to add a physic presence of a node in the physic simulation.
    /// </summary>
    public abstract class PhysicActor : Component
    {
        protected float _xRemainder;
        protected float _yRemainder;

        /// <summary> Main <see cref="Colliders.Collider"/> of the actor </summary>
        public virtual Collider? Collider { get; private set; }
    }
}
