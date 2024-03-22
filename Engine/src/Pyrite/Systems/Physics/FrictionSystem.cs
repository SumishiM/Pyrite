using Ignite.Attributes;
using Ignite.Systems;
using Pyrite.Components;
using Pyrite.Components.Physics;
using Pyrite.Utils;

namespace Pyrite.Systems.Physics
{
    [FilterComponent(typeof(VelocityComponent), typeof(FrictionComponent))]
    public readonly struct FrictionSystem : IFixedUpdateSystem
    {
        public void FixedUpdate(Context context)
        {
            foreach (var (First, Second) in context.Get<VelocityComponent, FrictionComponent>())
            {
                VelocityComponent velocity = First;
                FrictionComponent friction = Second;

                // Use velocity and friction here
                velocity.Velocity *= 1f - (friction.Amount * Time.FixedDeltaTime);
            }
        }
    }
}
