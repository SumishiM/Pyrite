using Ignite;
using Pyrite;
using Pyrite.Core;
using Pyrite.Graphics;
using Pyrite.Graphics.Rendering;

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

        protected override List<Type> Systems => [

        ];

        protected override void Initialize()
        {
            PercistentWorld.AddNode("Toothless");

            CurrentScene = new Scene(
                typeof(DefaultRendererSystem),
                typeof(SpinSystem));


            Node.CreateBuilder(CurrentScene.World, "Toothless")
                .AddComponent<SpriteComponent>(new("Content\\toothless.png"))
                .AddComponent<SpinComponent>()
                .ToNode();

            CurrentScene.World.AddNode("Main Camera", typeof(CameraComponent));

        }
    }
}
