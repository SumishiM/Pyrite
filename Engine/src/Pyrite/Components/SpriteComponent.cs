using Ignite.Components;
using Pyrite.Graphics;

namespace Pyrite.Components
{
    public class SpriteComponent : IComponent
    {
        public Sprite Sprite;

        public SpriteComponent(Sprite sprite)
        {
            Sprite = sprite;
        }

        public static implicit operator SpriteComponent ( Sprite sprite ) => new(sprite);
        public static implicit operator Sprite ( SpriteComponent sprite ) => sprite.Sprite;
    }
}
