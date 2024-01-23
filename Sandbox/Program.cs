using Pyrite;
using Pyrite.Core.Graphics.Rendering;
using System.Drawing;

namespace Sandbox
{
    internal class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            using Game game = new SandboxGame();
            game.Run();
        }
    }

    internal class SandboxGame : Game
    {
        protected override WindowInfo WindowInfo => new()
        {
            Title = "Pyrite Sandbox",
            Width = 1080,
            Height = 720,
            BackgroundColor = Color.Black,
            Maximized = false,
            Resizable = true,
        };

        public SandboxGame()
        {
            Renderer = new OGLRenderer();
        }

        protected override void Initialize()
        {
        }
    }
}
