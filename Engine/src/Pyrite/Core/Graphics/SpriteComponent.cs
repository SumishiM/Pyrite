using Ignite.Attributes;
using Pyrite.Components;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace Pyrite.Core.Graphics
{
    [RequireComponent(typeof(TransformComponent))]
    public struct SpriteComponent : IDrawable
    {
        public Texture Texture { get; init; } = Texture.Empty;

        public int SortingOrder { get; set; }

        /// <summary>
        /// Create sprite as empty
        /// </summary>
        public SpriteComponent()
        {
            Texture = Texture.Empty;
        }

        /// <summary>
        /// Create a sprite from path
        /// </summary>
        /// <param name="path"></param>
        public SpriteComponent(string path)
        {
            Texture = Texture.Create(path);
        }

        /// <summary>
        /// Create sprite from texture
        /// </summary>
        /// <param name="texture"></param>
        public SpriteComponent([NotNull] Texture texture)
        {
            Texture = texture;
        }

        public void Dispose()
        {

            GC.SuppressFinalize(this);
        }
    }
}
