using Pyrite;
using Pyrite.Core;
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

        protected override void Initialize()
        {
            Renderer = new OGLRenderer();

            Sprite sprite = new(
                "Content\\toothless.png",
                new Transform()
                {
                    Rotation = 45f
                });

            Camera.Main.Zoom = 1f;
            Renderer.Queue(sprite);
        }
    }
}
