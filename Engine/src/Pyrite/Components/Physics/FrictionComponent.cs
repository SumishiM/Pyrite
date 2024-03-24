using Ignite.Attributes;
using Ignite.Components;

namespace Pyrite.Components.Physics
{
    [RequireComponent(typeof(VelocityComponent))]
    public class FrictionComponent : IComponent
    {
        /// <summary>
        /// Friction amount
        /// </summary>
        public float Amount;

        public FrictionComponent()
        {
            Amount = 0.5f;
        }

        public FrictionComponent(float amount = 0.5f)
        {
            Amount = amount;
        }
    }
}
