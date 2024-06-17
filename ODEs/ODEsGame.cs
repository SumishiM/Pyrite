using Pyrite;
using Pyrite.Core;
using Pyrite.Systems.Graphics;

namespace ODEs
{
    public partial class ODEsGame : IPyriteGame
    {
        public string Name => "Ordinary Differential Equations";

        public WindowInfo GameWindowInfo => WindowInfo.Default with
        {
            MinimalWindowedSize = new(1080, 720)
        };

        protected void Initialized()
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