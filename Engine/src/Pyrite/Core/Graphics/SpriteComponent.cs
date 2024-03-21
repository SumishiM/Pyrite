using Ignite.Attributes;
using Pyrite.Components;
using Pyrite.Core.Graphics.Rendering.OpenGL;
using System.IO;

namespace Pyrite.Core.Graphics
{
    [RequireComponent(typeof(TransformComponent))]
    public struct SpriteComponent : IDrawable
    {
        public Shader? Shader { get; set; } = null;
        public Texture Texture { get; init; }

        public int SortingOrder { get; set; }

        /// <summary>
        /// Create sprite as empty
        /// </summary>
        public SpriteComponent()
        {
            Texture = Texture.Create("Content\\Empty.png");
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
        public SpriteComponent(Texture texture)
        {
            Texture = texture;
        }

        public void Dispose()
        {
            Shader?.Dispose();
            Texture?.Dispose();

            GC.SuppressFinalize(this);
        }
    }
}
