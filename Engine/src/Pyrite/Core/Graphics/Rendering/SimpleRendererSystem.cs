using Ignite;
using Ignite.Attributes;
using Ignite.Systems;
using Pyrite.Core.Components;
using Pyrite.Utils;

namespace Pyrite.Core.Graphics.Rendering
{
    [FilterComponent(typeof(SpriteComponent))]
    public class SimpleRendererSystem : 
        OGLRenderer, IStartSystem, IRenderSystem
    {
        public void Start ( Context context )
        {
            Initialize();
        }

        public void Render ( Context context )
        {
            if ( Camera.Main == null )
                throw new NullReferenceException("No main camera found for render.");

            ClearScreen();

            foreach ( var node in context.Nodes )
            {
                Draw(node.GetComponent<SpriteComponent>());
            }
        }
    }
}
