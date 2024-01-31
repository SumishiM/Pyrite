using Ignite;
using Pyrite;
using Pyrite.Core;
using Pyrite.Components;
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
            typeof(SimpleRendererSystem),
            typeof(SpinSystem)
        ];

        protected override void Initialize()
        {
            var Toothless =
                Node.CreateBuilder(PercistentWorld, "Toothless")
                    .AddComponent<SpriteComponent>(new("Content\\toothless.png"))
                    .AddComponent<SpinComponent>();

            PercistentWorld.AddNode(Toothless);
        }
    }
}
