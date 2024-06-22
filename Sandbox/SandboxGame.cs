using Ignite;
using Pyrite;
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
                    typeof(DefaultRendererSystem),
                    typeof(SpinSystem),
                    typeof(VelocitySystem),
                    typeof(FrictionSystem)
                    );

            SceneManager.LoadScene(ref toothlessScene);

            var builder = Node.CreateBuilder(SceneManager.CurrentScene.World, "Toothless")
                    .AddComponent<SpriteComponent>(new("Content\\toothless.png"))
                    .AddComponent<SpinComponent>(new(0f))
                    .AddComponent<VelocityComponent>(new(20, 0))
                    .AddComponent<FrictionComponent>(new(0.5f));

            Node toothless = builder.ToNode();

            Console.WriteLine(Camera.Main.Top);
            Console.WriteLine(Camera.Main.Bottom);
            Console.WriteLine(Camera.Main.Left);
            Console.WriteLine(Camera.Main.Right);

            toothless.GetComponent<TransformComponent>().Position = new Vector2(0, 0);
        }
    }
}