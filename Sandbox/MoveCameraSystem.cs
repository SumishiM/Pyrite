using Ignite.Attributes;
using Ignite.Systems;
using Pyrite.Components.Graphics;
using Pyrite.Core.Geometry;
using Pyrite.Core.Graphics;
using Pyrite.Utils;

namespace Sandbox
{
    [FilterComponent(typeof(CameraComponent))]
    public readonly struct MoveCameraSystem : IUpdateSystem
    {
        public readonly void Update(Context context)
        {
            //foreach (var camera in context.Get<CameraComponent>())
            {
                Camera.Main.Transform.Rotation += 20f * Time.DeltaTime;
                Camera.Main.Transform.Position = Vector2.UnitX * 200f * MathF.Cos(Time.TotalTime);
            }
        }
    }
}