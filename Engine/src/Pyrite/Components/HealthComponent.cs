using Ignite.Components;

namespace Pyrite.Components
{
    public class HealthComponent : IComponent
    {
        public float Health = 0f;
        public float MaxHealth = 0f;

        public event Action? OnDeath = null; 
        
        public HealthComponent() {}
    }
}