using Ignite;
using Ignite.Attributes;
using Ignite.Systems;
using Pyrite.Components;

namespace Pyrite.Graphics.Rendering
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
            if ( CameraComponent.Main == null )
                throw new NullReferenceException("No main camera found for render.");

            ClearScreen();

            foreach ( var node in context.Nodes )
            {
                Draw(node.GetComponent<SpriteComponent>());
            }
        }
    }
}
