using Ignite.Attributes;
using Ignite.Systems;
using Pyrite.Core;
using Pyrite.Utils;

namespace ODEs
{
    [FilterComponent(typeof(LifeTimeComponent))]
    public struct LifeTimeSystem : IUpdateSystem
    {
        public void Update(Context context)
        {
            var components = context.Get<LifeTimeComponent>();

            for (int i = components.Length - 1; i >= 0; i--)
            {
                components[i].LifeTime -= Time.DeltaTime;
                
                if (components[i].LifeTime <= 0f)
                {
                    context.Nodes[i].Destroy();
                }
            }
        }
    }
}
