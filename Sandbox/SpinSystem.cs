using Ignite.Attributes;
using Ignite.Systems;
using Pyrite.Components;
using Pyrite.Core;
using Pyrite.Utils;

namespace Sandbox
{
    [FilterComponent(Context.AccessFilter.AllOf, 
        typeof(SpinComponent), 
        typeof(TransformComponent))]
    public class SpinSystem : IFixedUpdateSystem
    {
        public void FixedUpdate( Context context )
        {
            foreach ( var node in context.Nodes )
            {
                Transform transform = node.GetComponent<TransformComponent>();
                transform.Rotation += node.GetComponent<SpinComponent>().SpinSpeed * Time.FixedDeltaTime;
            }
        }

        public void Dispose ()
        {
        }
    }
}
