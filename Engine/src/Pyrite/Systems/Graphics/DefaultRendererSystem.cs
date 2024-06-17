using Ignite.Attributes;
using Ignite.Systems;
using Pyrite.Components;
using Pyrite.Components.Graphics;
using Pyrite.Core.Geometry;
using Pyrite.Core.Graphics;
using Pyrite.Utils;
using Microsoft.Xna.Framework.Graphics;

namespace Pyrite.Systems.Graphics
{
    [FilterComponent(Context.AccessFilter.AllOf, typeof(SpriteComponent))]
    [FilterComponent(Context.AccessFilter.NoneOf, typeof(InvisibleComponent))]
    public partial class DefaultRendererSystem : IRenderSystem
    {
        public void Render(Context context)
        {
            if (Camera.Main == null)
                throw new NullReferenceException("No main camera found for render.");

            //ClearScreen();
            Game.GraphicsDevice.Clear(Color.Black);
            Game.Instance.SpriteBatch.Begin();

            foreach (var (sprite, transform) in context.Get<SpriteComponent, TransformComponent>())
            {
                Game.Instance.SpriteBatch.Draw(
                    sprite.AssetRef.Asset.Texture, 
                    transform.Position,
                    null,
                    Color.White, 
                    transform.Rotation,
                    (Vector2)sprite.AssetRef.Asset.Texture.Size() / 2f,
                    transform.Scale,
                    SpriteEffects.None,
                    sprite.ZOrder);
            }
            
            Game.Instance.SpriteBatch.End();
        }
    }
}
