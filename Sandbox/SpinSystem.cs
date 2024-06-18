using Ignite.Attributes;
using Ignite.Systems;
using Pyrite.Components;
using Pyrite.Utils;

namespace Sandbox
{
    [FilterComponent(typeof(SpinComponent))]
    public readonly struct SpinSystem : IUpdateSystem
    {
        public readonly void Update(Context context)
        {
            foreach (var (spin, transform) in context.Get<SpinComponent, TransformComponent>())
            {
                transform.Rotation += spin.SpinSpeed * Time.DeltaTime;
            }
        }
    }
}