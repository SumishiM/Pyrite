using Ignite.Attributes;
using Ignite.Systems;
using Pyrite.Components;
using Pyrite.Core.Graphics;
using Pyrite.Core.Graphics.Rendering.OpenGL;
using Silk.NET.Vulkan;

namespace Pyrite.Systems.Graphics
{
    [FilterComponent(typeof(SpriteComponent))]
    public class DefaultRendererSystem :
        OGLRenderer, IStartSystem, IRenderSystem
    {
        public void Start(Context context)
        {
            Initialize();
        }

        public void Render(Context context)
        {
            if (Camera.Main == null)
                throw new NullReferenceException("No main camera found for render.");

            ClearScreen();

            foreach (var node in context.Nodes)
            {
                Draw(node.GetComponent<SpriteComponent>());
            }
        }
    }
}
