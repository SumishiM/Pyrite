using Ignite.Attributes;
using Ignite.Systems;
using Pyrite.Components.Graphics;

namespace Pyrite.Systems.Graphics
{
    [FilterComponent(Context.AccessFilter.AllOf, typeof(SpriteStackComponent))]
    [FilterComponent(Context.AccessFilter.NoneOf, typeof(InvisibleComponent))]
	public struct StackRendererSystem : IRenderSystem
	{
		public void Render(Context context)
		{
            
		}
	}
}