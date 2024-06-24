using Ignite.Attributes;
using Ignite.Systems;
using Microsoft.Xna.Framework.Graphics;
using Pyrite.Components;
using Pyrite.Components.Graphics;
using Pyrite.Core.Geometry;
using Pyrite.Core.Graphics;
using Pyrite.Utils;

namespace Pyrite.Systems.Graphics
{
    /// <summary>
    /// Simple sprite renderer
	/// <para>
	/// Filters : <see cref="SpriteComponent"/>
	/// </para>
    /// </summary>
    [FilterComponent(Context.AccessFilter.AllOf, typeof(SpriteComponent))]
    [FilterComponent(Context.AccessFilter.NoneOf, typeof(InvisibleComponent))]
    public struct SpriteRendererSystem : IRenderSystem
    {
		/// <summary>
		/// Game render target
		/// </summary>
        private RenderTarget2D? _renderTarget;

        public void Render(Context context)
        {
            _renderTarget ??= new RenderTarget2D(Game.GraphicsDevice, Game.Settings.GameWidth, Game.Settings.GameHeight);

            // ? May be useful later when i make a custom sprite batch or rendering system
            //var sprites = context.Get<SpriteComponent, TransformComponent>();
            //sprites = [.. sprites.OrderBy(s => s.First.ZOrder)];

            Game.Instance.SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Opaque, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullNone);

            // set drawing on the game render target
            Game.GraphicsDevice.SetRenderTarget(_renderTarget);

            // Draw every sprites in the default FNA sprite batch
            // this may change once i start making my own sprite batches (not planned for now tho)
            foreach (var (sprite, transform) in context.Get<SpriteComponent, TransformComponent>())
            {
                Game.Instance.SpriteBatch.Draw(
                    sprite.Texture,
                    transform.Position,
                    null,
                    sprite.Color,
                    transform.Rotation.ToRadians(),
                    sprite.Texture.Bounds.Center.ToXnaVector2(),
                    Vector2.One,
                    SpriteEffects.None,
                    sprite.ZOrder);
            }

            // end batch to finish the draw on the render target
            Game.Instance.SpriteBatch.End();

            Game.Instance.SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Opaque, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullNone);
            
            // draw game to screen
            // set the render target to null to draw on window
            Game.GraphicsDevice.SetRenderTarget(null);

            float factor = (float)Game.Instance.Window.Width / (float)Game.Settings.GameWidth;

            // draw the game render target upscaled on window
            Game.Instance.SpriteBatch.Draw(
                _renderTarget,
                new Microsoft.Xna.Framework.Rectangle(0, 0, (int)(factor * Game.Settings.GameWidth), (int)(factor * Game.Settings.GameHeight)), 
                Color.White);

            Game.Instance.SpriteBatch.End();
        }
    }
}