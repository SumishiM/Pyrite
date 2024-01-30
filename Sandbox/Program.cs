using Ignite;
using Ignite.Systems;
using Pyrite;
using Pyrite.Core;
using Pyrite.Core.Components;
using Pyrite.Core.Graphics;
using Pyrite.Core.Graphics.Rendering;
using System.Drawing;
using System.Numerics;

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
            Sprite sprite = new(
                "Content\\toothless.png",
                new Transform()
                {
                    Rotation = 45f
                });

            var Toothless =
                Node.CreateBuilder(PercistentWorld, "Toothless")
                    .AddComponent<SpriteComponent>(sprite)
                    .AddComponent<SpinComponent>();

            PercistentWorld.AddNode(Toothless);
        }
    }
}
