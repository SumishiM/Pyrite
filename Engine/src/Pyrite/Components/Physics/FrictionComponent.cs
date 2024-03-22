using Ignite.Components;

namespace Pyrite.Components.Physics
{
    public class FrictionComponent : IComponent
    {
        public float Amount;

        public FrictionComponent(float amount)
        {
            Amount = amount;
        }
    }
}
