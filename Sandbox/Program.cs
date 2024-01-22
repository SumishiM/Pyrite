using Pyrite;
using Pyrite.Core.Graphics.Rendering;

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

    internal class SandboxGame : Game, IPyriteGame
    {
        public string Name => "Sandbox";

        public SandboxGame()
        {
            Renderer = new OGLRenderer();
        }

        protected override void Initialize()
        {
        }
    }
}
