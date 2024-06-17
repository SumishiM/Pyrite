using Ignite.Attributes;
using Ignite.Systems;
using Microsoft.Xna.Framework.Graphics;
using Pyrite.Components;
using Pyrite.Components.Graphics;
using Pyrite.Core.Graphics;

namespace Pyrite.Systems.Graphics
{
    [FilterComponent(Context.AccessFilter.AllOf, typeof(SpriteComponent))]
    [FilterComponent(Context.AccessFilter.NoneOf, typeof(InvisibleComponent))]
    public partial class DefaultRendererSystem : IRenderSystem
    {
        public void Start(Context context)
        {
            //Initialize();
        }

        public void Render(Context context)
        {
            if (Camera.Main == null)
                throw new NullReferenceException("No main camera found for render.");

            //ClearScreen();

            foreach (var (sprite, transform) in context.Get<SpriteComponent, TransformComponent>())
            {
                //Draw(transform, sprite.Texture, sprite.Shader);
            }
        }
    }
}
