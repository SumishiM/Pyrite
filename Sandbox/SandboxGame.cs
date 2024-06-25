using Ignite;
using Pyrite;
using Pyrite.Assets;
using Pyrite.Components;
using Pyrite.Components.Graphics;
using Pyrite.Core;
using Pyrite.Core.Geometry;
using Pyrite.Systems.Graphics;
using Pyrite.Systems.Physics;

namespace Sandbox
{
    public partial class SandboxGame : IPyriteGame
    {
        public WindowInfo GameWindowInfo => WindowInfo.Default with
        {
            MinimalWindowedSize = new(1080, 720)
        };

        public string Name => "Pyrite Sandbox YEPEE";

        public void Initialize()
        {
            Scene toothlessScene =
                new("Toothless scene",
                    typeof(SpriteStackRendererSystem),
                    //typeof(MoveCameraSystem),
                    typeof(SpinSystem),
                    typeof(VelocitySystem),
                    typeof(FrictionSystem)
                    );

            SceneManager.LoadScene(ref toothlessScene);

            Node car =
                Node.CreateBuilder(SceneManager.CurrentScene.World, "Car")
                    //[.. Enumerable.Range(0, 7).Select(i => $"Content\\Car\\Car{i}.png")]
                    .AddComponent<SpriteStackComponent>(new([.. from i in Enumerable.Range(0, 7) select $"Content\\Car\\Car{i}.png"]))
                    .AddComponent<SpinComponent>(new(0f));

            car.GetComponent<TransformComponent>().Position = new Vector2(160, 90);

            SceneManager.CurrentScene.World.Q<SpriteStackComponent, TransformComponent>((c) =>
            {
                foreach (var (stack, transform) in c.Get<SpriteStackComponent, TransformComponent>())
                {
                    transform.Rotation = 45f * 3f;
                }
            }, true);
        }

        public void LoadContent()
        {
            for (int i = 0; i < 7; i++)
            {
                Game.Data.TryAddAsset(new TextureAsset($"Content\\Car\\Car{i}.png"));
            }
        }
    }
}