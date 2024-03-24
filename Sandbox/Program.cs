using Ignite;
using Pyrite;
using Pyrite.Assets;
using Pyrite.Components;
using Pyrite.Components.Physics;
using Pyrite.Core;
using Pyrite.Core.Geometry;
using Pyrite.Core.Graphics;
using Pyrite.Systems.Graphics;
using Pyrite.Systems.Physics;

namespace Sandbox
{
    internal class Program
    {
        [STAThread]
#if DEBUG
        static void Main(string[] args)
#else
        static void Main()
#endif
        {
            using Game game = new SandboxGame();
            game.Run();
        }
    }

    public partial class SandboxGame : Game
    {
        protected override WindowInfo WindowInfo => base.WindowInfo with
        {
            Title = "Pyrite Sandbox YEPEE"
        };

        protected override void Initialize()
        {
            Scene toothlessScene =
                new("Toothless scene",
                    typeof(DefaultRendererSystem),
                    typeof(SpinSystem),
                    typeof(VelocitySystem),
                    typeof(FrictionSystem)
                    );

            SceneManager.LoadScene(ref toothlessScene);

            Node toothless =
                Node.CreateBuilder(SceneManager.CurrentScene.World, "Toothless")
                    .AddComponent<SpriteComponent>(new("Content\\toothless.png"))
                    .AddComponent<SpinComponent>(new() { SpinSpeed = 36f })
                    .AddComponent<VelocityComponent>(new Vector2(250f))
                    .AddComponent<FrictionComponent>(new(0.5f));

            toothless.GetComponent<TransformComponent>().Position = new Vector2(300, 300);
        }
    }
}
