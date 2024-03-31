using Ignite.Attributes;
using Ignite.Systems;
using Pyrite.Components;
using Pyrite.Components.Physics;
using Pyrite.Utils;

namespace Pyrite.Systems.Physics
{
    [FilterComponent(typeof(HealthComponent))]
    public readonly struct HealthSystem : IStartSystem
    {
        public void Start(Context context)
        {
            foreach (var health in context.Get<HealthComponent>())
            {
                health.Health = health.MaxHealth;
            }
        }
    }
}
