using Ignite.Attributes;
using Ignite.Components;
using Microsoft.Xna.Framework.Graphics;
using Pyrite.Assets;
using Pyrite.Core.Graphics;
using System.Diagnostics.CodeAnalysis;

namespace Pyrite.Components.Graphics
{
    /// <summary>
    /// Default Pyrite sprite component.
    /// <para>
    /// Filtered by : <see cref="Pyrite.Systems.Graphics.SpriteRendererSystem"/>
    /// </para>
    /// </summary>
    [RequireComponent(typeof(TransformComponent))]
    public struct SpriteComponent : IComponent
    {
        /// <summary>
        /// <see cref="TextureAsset"/> reference
        /// </summary>
        public readonly AssetReference<TextureAsset> AssetRef;

        /// <summary>
        /// Sprite <see cref="Texture2D"/> or the default engine missing texture
        /// </summary>
        public readonly Texture2D Texture => AssetRef.TryAsset?.Texture ?? Game.Data.MissingTextureAssetRef.Asset.Texture; 

        /// <summary>
        /// Sprite render color
        /// </summary>
        public Color Color = Color.White;

        /// <summary>
        /// Z Display order
        /// </summary>
        public float ZOrder { get; set; }

        /// <summary>
        /// Create sprite as empty
        /// </summary>
        public SpriteComponent()
        {
            AssetRef = Game.Data.MissingTextureAssetRef;
            ZOrder = 0;
        }

        /// <summary>
        /// Create a sprite from path
        /// </summary>
        /// <param name="path">Asset file path</param>
        public SpriteComponent(string path, float zOrder = 0f)
        {
            AssetRef = new(Game.Data.GetOrCreateAsset<TextureAsset>(path).Guid);
            ZOrder = zOrder;
        }

        /// <summary>
        /// Create a sprite from a <see cref="Guid"/>
        /// </summary>
        /// <param name="guid">Asset <see cref="Guid"/></param>
        public SpriteComponent(Guid guid, float zOrder = 0f)
        {
            if (guid == Guid.Empty)
            {
                AssetRef = new(Game.Data.MissingTextureGuid);
            }
            else
            {
                AssetRef = new(guid);
            }
            ZOrder = zOrder;
        }

        /// <summary>
        /// Create sprite from a <see cref="TextureAsset"/>
        /// </summary>
        /// <param name="texture">Texture asset</param>
        public SpriteComponent([NotNull] TextureAsset texture, float zOrder = 0f)
        {
            AssetRef = new(texture.Guid);
            ZOrder = zOrder;
        }
    }
}
