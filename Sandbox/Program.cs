using Ignite;
using Pyrite;
using Pyrite.Components;
using Pyrite.Core;
using Pyrite.Core.Geometry;
using Pyrite.Core.Graphics;
using Pyrite.Systems.Graphics;

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

    internal class SandboxGame : Game
    {
        protected override WindowInfo WindowInfo => base.WindowInfo with
        {
            Title = "Pyrite Sandbox YEPEE"
        };

        protected override void Initialize()
        {
            Scene toothlessScene = new("Toothless scene",
                    typeof(DefaultRendererSystem),
                    typeof(SpinSystem));

            SceneManager.LoadScene(ref toothlessScene);

            Node toothless =
                Node.CreateBuilder(SceneManager.CurrentScene.World, "Toothless")
                    .AddComponent<SpriteComponent>(new("Content\\toothless.png"))
                    .AddComponent<SpinComponent>(new() { SpinSpeed = 36f });

            toothless.GetComponent<TransformComponent>().Position = new Vector2(300, 300);
        }
    }
}
