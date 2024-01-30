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

            Sprite pyriteLogo = new(
                "Content\\PyriteIcon512.png",
                new Transform()
                {
                    Position = new Vector2(200f, 200f),
                    Rotation = 180f
                });
            Camera.Main.Zoom = 4f;
            Camera.Main.Position = new Vector2(200f, 200f);
            Renderer.Queue(pyriteLogo);
        }
    }
}
