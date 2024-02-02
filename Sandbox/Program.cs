﻿using Ignite;
using Pyrite;
using Pyrite.Core;
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
            SceneManager.LoadScene(
                new Scene("Toothless scene",
                    typeof(DefaultRendererSystem),
                    typeof(SpinSystem)));

            Node.CreateBuilder(SceneManager.CurrentScene.World, "Toothless")
                .AddComponent<SpriteComponent>(new("Content\\toothless.png"))
                .AddComponent<SpinComponent>()
                .ToNode();
        }
    }
}
