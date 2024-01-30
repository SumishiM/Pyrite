using Ignite.Attributes;
using Ignite.Systems;
using Pyrite.Core.Components;
using Pyrite.Core.Graphics;
using Pyrite.Utils;
using System.Numerics;

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
                sprite.Transform.Rotation += 720f * MathF.Cos(Time.TotalTime) * Time.DeltaTime;
                sprite.Transform.Position = 
                    Vector2.UnitX * 360f * MathF.Cos(Time.TotalTime * 5f) + 
                    Vector2.UnitY * 180f * MathF.Sin(Time.TotalTime * 5f);
            }
        }

        public void Dispose ()
        {
        }
    }
}
