using Pyrite;
using Pyrite.Core.Rendering;

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
        readonly private OGLRenderer _renderer;
        public string Name => "Sandbox";

        public SandboxGame()
        {
            _renderer = new OGLRenderer();
        }

        protected override void Initialize()
        {
            _renderer.Initialize();
        }

        protected override void Draw()
        {
            _renderer.Draw();
        }
    }
}
