using Ignite;
using Pyrite;
using Pyrite.Components.Physics;
using Pyrite.Components;
using Pyrite.Core;
using Pyrite.Core.Graphics;
using Pyrite.Systems.Graphics;
using Pyrite.Core.Geometry;
using Pyrite.Components.Graphics;

namespace ODEs
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
            using Game game = new ODEsGame();
            game.Run();
        }
    }

    public partial class ODEsGame : Game
    {
        protected override WindowInfo WindowInfo => base.WindowInfo with
        {
            Title = "Ordinary Differential Equations",
            Size = new(800, 600),
        };

        protected override void Initialize()
        {
            Scene MainScene =
                new("ODEs",
                    typeof(DefaultRendererSystem),
                    typeof(Attractor1),
                    typeof(ParticleSpawner),
                    typeof(LifeTimeSystem)
            );

            SceneManager.LoadScene(ref MainScene);
        }
    }
}