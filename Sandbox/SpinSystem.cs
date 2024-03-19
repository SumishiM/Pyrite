using Ignite.Attributes;
using Ignite.Systems;
using Pyrite.Components;
using Pyrite.Core;
using Pyrite.Utils;

namespace Sandbox
{
    [FilterComponent(typeof(SpinComponent))]
    public readonly struct SpinSystem : IFixedUpdateSystem
    {
        public readonly void FixedUpdate(Context context)
        {
            foreach (var (spin, transform) in context.Get<SpinComponent, TransformComponent>())
            {
                transform.Rotation += spin.SpinSpeed * Time.FixedDeltaTime;
            }
        }
    }
}