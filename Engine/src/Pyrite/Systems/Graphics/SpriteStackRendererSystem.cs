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
	/// Sprite stack renderer, rendering entire stacks of sprites with an adjustable spread
	/// <para>
	/// Filters : <see cref="SpriteStackComponent"/>
	/// </para>
	/// </summary>
	[FilterComponent(Context.AccessFilter.AllOf, typeof(SpriteStackComponent))]
	[FilterComponent(Context.AccessFilter.NoneOf, typeof(InvisibleComponent))]
	public struct SpriteStackRendererSystem : IRenderSystem
	{
		/// <summary>
		/// Game render target
		/// </summary>
		private RenderTarget2D? _renderTarget;

		/// <summary>
		/// Spacing in between each stack layers
		/// <para>
		/// May move in <see cref="SpriteStackComponent"/>
		/// </para> 
		/// </summary>
		public float Spread = 1f;

		public SpriteStackRendererSystem() { }

		public SpriteStackRendererSystem(float spread)
		{
			Spread = spread;
		}

		public void Render(Context context)
		{
			/// note : this is basically the same thing as <see cref="SpriteRendererSystem.Render(Context)"/> 

			_renderTarget ??= new RenderTarget2D(Game.GraphicsDevice, Game.Settings.GameWidth, Game.Settings.GameHeight);
			Game.Instance.SpriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.DepthRead, RasterizerState.CullNone);

			// set drawing on the game render target
			Game.GraphicsDevice.SetRenderTarget(_renderTarget);

			foreach (var (stack, transform) in context.Get<SpriteStackComponent, TransformComponent>())
			{
				// if null go to the next sprite stack
				if (stack.IsEmpty) continue;

				var textures = stack.Textures;

				for (int i = 0; i < textures!.Length; i++)
				{
					Game.Instance.SpriteBatch.Draw(
						textures[i],
						transform.Position - Vector2.UnitY * i * Spread, // up from one stack unit
						null,
						stack.Color,
						transform.Rotation.ToRadians(),
						stack.Center,
						Vector2.One,
						SpriteEffects.None,
						(stack.ZOrder + i * Spread) / 100f); // up from one stack unit to respect depth foreach stacks
				}
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