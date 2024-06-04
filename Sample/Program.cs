using Pyrite;
using Pyrite.Core;
using Pyrite.Systems.Graphics;

namespace Sample
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
            using Game game = new SampleGame();
            game.Run();
        }
    }

    public partial class SampleGame : Game
    {
        /// <summary>
        /// Define window infos
        /// </summary>
        protected override WindowInfo WindowInfo => base.WindowInfo with
        {
            Title = "Pyrite Sample",
            Size = new(800, 600),
            // other window settings ...
        };

        /// <summary>
        /// Setup the game
        /// </summary>
        protected override void Initialize()
        {
            // Create default scene
            Scene MainScene =
                new("SampleScene",
                    typeof(DefaultRendererSystem),
                    typeof(SamplePercistantSystem)
            );

            // Load default scene
            SceneManager.LoadScene(ref MainScene);
        }
    }
}