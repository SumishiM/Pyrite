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
            foreach (var node in context.Nodes)
            {
                Transform transform = node.GetComponent<TransformComponent>();
                transform.Rotation += node.GetComponent<SpinComponent>().SpinSpeed * Time.FixedDeltaTime;
            }
        }
    }
}