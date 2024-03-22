using Ignite.Attributes;
using Ignite.Systems;
using Pyrite.Components;
using Pyrite.Components.Physics;
using Pyrite.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pyrite.Systems.Physics
{
    [FilterComponent(typeof(TransformComponent), typeof(VelocityComponent))]
    public readonly struct VelocitySystem : IFixedUpdateSystem
    {
        public void FixedUpdate(Context context)
        {
            foreach (var (transform, velocity) in context.Get<TransformComponent, VelocityComponent>())
            {
                transform.Position += velocity.Velocity * Time.FixedDeltaTime;
                Console.WriteLine(velocity.Velocity);
            }
        }
    }
}
