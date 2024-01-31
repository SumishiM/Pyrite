using Ignite.Attributes;
using Ignite.Systems;
using Pyrite.Core.Components;
using Pyrite.Core.Graphics;
using Pyrite.Utils;

namespace Sandbox
{
    [FilterComponent(typeof(SpinComponent))]
    public class SpinSystem : IUpdateSystem
    {
        public void Update ( Context context )
        {
            foreach ( var node in context.Nodes )
            {
                Sprite sprite = node.GetComponent<SpriteComponent>();

                sprite.Transform.Rotation += 
                    node.GetComponent<SpinComponent>().SpinSpeed * Time.DeltaTime;
            }
        }

        public void Dispose ()
        {
        }
    }
}
