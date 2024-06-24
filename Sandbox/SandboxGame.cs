using Ignite;
using Pyrite;
using Pyrite.Assets;
using Pyrite.Components;
using Pyrite.Components.Graphics;
using Pyrite.Components.Physics;
using Pyrite.Core;
using Pyrite.Core.Geometry;
using Pyrite.Core.Graphics;
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
                    typeof(SpinSystem),
                    typeof(VelocitySystem),
                    typeof(FrictionSystem)
                    );

            SceneManager.LoadScene(ref toothlessScene);

            Node car =
                Node.CreateBuilder(SceneManager.CurrentScene.World, "Car")
                    .AddComponent<SpriteStackComponent>(new([
                        "Content\\Car\\Car0.png",
                        "Content\\Car\\Car1.png",
                        "Content\\Car\\Car2.png",
                        "Content\\Car\\Car3.png",
                        "Content\\Car\\Car4.png",
                        "Content\\Car\\Car5.png",
                        "Content\\Car\\Car6.png",
                    ]))
                    .AddComponent<SpinComponent>(new(45f));
            car.GetComponent<TransformComponent>().Position = new Vector2(160, 90);
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