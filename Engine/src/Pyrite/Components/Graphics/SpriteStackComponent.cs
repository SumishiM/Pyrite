using Ignite.Attributes;
using Ignite.Components;
using Microsoft.Xna.Framework.Graphics;
using Pyrite.Assets;
using Pyrite.Core.Geometry;
using Pyrite.Core.Graphics;
using Pyrite.Utils;

namespace Pyrite.Components.Graphics
{
    /// <summary>
    /// Default Pyrite sprite component.
    /// <para>
    /// Filtered by : <see cref="Pyrite.Systems.Graphics.StackRendererSystem"/>
    /// </para>
    /// </summary>
    [RequireComponent(typeof(TransformComponent))]
    public struct SpriteStackComponent : IComponent
    {
        /// <summary>
        /// <see cref="TextureAsset"/> reference
        /// </summary>
        public readonly AssetReference<TextureAsset>[]? AssetRefs = null;

        /// <summary>
        /// Sprite <see cref="Texture2D"/> or the default engine missing texture
        /// </summary>
        public readonly Texture2D[]? Textures
        {
            get
            {
                // check if there is some assets
                if (IsEmpty) return null;

                var textures = new Texture2D[AssetRefs!.Length];

                // populate array with textures from database [ O(1) access ]
                for (int i = 0; i < AssetRefs.Length; i++)
                {
                    textures[i] = AssetRefs[i].TryAsset?.Texture ?? Game.Data.MissingTextureAssetRef.Asset.Texture;
                }

                return textures;
            }
        }

        private readonly Vector2 _center;
        public readonly Vector2 Center => _center;

        /// <summary>
        /// Sprite render color
        /// </summary>
        public Color Color = Color.White;

        /// <summary>
        /// Z Display order at the base of the stack
        /// </summary>
        public float ZOrder { get; set; }

		/// <summary>
		/// Empty sprite stack
		/// </summary>
        public static SpriteStackComponent Empty => new();

		/// <summary>
		/// Whether the stack is empty or not, when there is no asset refs registered not atlases
		/// </summary>
        public readonly bool IsEmpty => AssetRefs is null || AssetRefs.Length == 0;

        /// <summary>
        /// Creat an empty Sprite stack
        /// </summary>
        public SpriteStackComponent()
        {
            ZOrder = 0;
        }

        /// <summary>
        /// Create a sprite from path
        /// </summary>
        /// <param name="paths">Asset file path array</param>
        public SpriteStackComponent(string[] paths, float zOrder = 0f)
        {
            AssetRefs = new AssetReference<TextureAsset>[paths.Length];

            for (int i = 0; i < paths.Length; i++)
            {
                AssetRefs[i] = new(Game.Data.GetOrCreateAsset<TextureAsset>(paths[i]).Guid);
            }

            _center = AssetRefs?[0].TryAsset?.Texture.Bounds.Center.ToVector2() ?? Vector2.Zero;

            ZOrder = zOrder;
        }

        /// <summary>
        /// Create a sprite stack from a <see cref="Guid"/> array
        /// </summary>
        /// <param name="guids">Asset <see cref="Guid"/> array </param>
        public SpriteStackComponent(Guid[] guids, float zOrder = 0f)
        {
            AssetRefs = new AssetReference<TextureAsset>[guids.Length];

            for (int i = 0; i < guids.Length; i++)
            {
                if (guids[i] == Guid.Empty)
                {
                    AssetRefs[i] = new(Game.Data.MissingTextureGuid);
                }
                else
                {
                    AssetRefs[i] = new(guids[i]);
                }
            }

            _center = AssetRefs?[0].TryAsset?.Texture.Bounds.Center.ToVector2() ?? Vector2.Zero;

            ZOrder = zOrder;
        }

        /// <summary>
        /// Create sprite stack from a <see cref="TextureAsset"/> array
        /// </summary>
        /// <param name="textures">Texture asset array</param>
        public SpriteStackComponent(TextureAsset[] textures, float zOrder = 0f)
        {
            AssetRefs = new AssetReference<TextureAsset>[textures.Length];

            for (int i = 0; i < textures.Length; i++)
            {
                AssetRefs[i] = new(textures[i].Guid);
            }
            
            _center = AssetRefs?[0].TryAsset?.Texture.Bounds.Center.ToVector2() ?? Vector2.Zero;

            ZOrder = zOrder;
        }
    }
}