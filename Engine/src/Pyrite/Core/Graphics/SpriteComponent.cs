using Ignite.Attributes;
using Ignite.Components;
using Pyrite.Assets;
using Pyrite.Components;
using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace Pyrite.Core.Graphics
{
    [RequireComponent(typeof(TransformComponent))]
    public struct SpriteComponent : IComponent
    {
        public AssetReference<TextureAsset> AssetRef;

        public float ZOrder { get; set; }

        /// <summary>
        /// Create sprite as empty
        /// </summary>
        public SpriteComponent()
        {
            AssetRef = Game.Data.MissingTextureAssetRef;
        }

        /// <summary>
        /// Create a sprite from path
        /// </summary>
        /// <param name="path">Asset file path</param>
        public SpriteComponent(string path)
        {
            AssetRef = new(Game.Data.GetOrCreateAsset<TextureAsset>(path).Guid);
        }

        /// <summary>
        /// Create a sprite from an asset <see cref="Guid"/>
        /// </summary>
        /// <param name="guid">Asset Guid</param>
        public SpriteComponent(Guid guid)
        {
            if (guid == Guid.Empty)
            {
                AssetRef = new(Game.Data.MissingTextureGuid);
            }
            else
            {
                AssetRef = new(guid);
            }
        }

        /// <summary>
        /// Create sprite a <see cref="TextureAsset"/>
        /// </summary>
        /// <param name="texture">Texture asset</param>
        public SpriteComponent([NotNull] TextureAsset texture)
        {
            AssetRef = new(texture.Guid);
        }
    }
}
