using Pyrite;
using Pyrite.Core;
using Pyrite.Systems.Graphics;

namespace Sample
{
    public partial class SampleGame : IPyriteGame
    {
        public WindowInfo GameWindowInfo => WindowInfo.Default with
        {
            MinimalWindowedSize = new(1080, 720)
        };

        public string Name => "Sample";

        public void Initialize()
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