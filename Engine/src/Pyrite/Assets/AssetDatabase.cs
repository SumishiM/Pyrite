using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace Pyrite.Assets
{
    public class AssetDatabase
    {
        internal Dictionary<Type, HashSet<Guid>> Lookup = new()
        {
            { typeof(Texture), [] }
        };

        internal Dictionary<Guid, GameAsset> AllAssets = [];
        internal Guid MissingTextureGuid;
        /// <summary>
        /// A shader specialized for rendering pixel art.
        /// </summary>
        public Effect? ShaderPixel = null;
        public Effect? ShaderSimple = null;
        public Effect? ShaderSprite = null;

        public AssetReference<TextureAsset> MissingTextureAssetRef = AssetReference<TextureAsset>.Empty;

        public AssetDatabase()
        {
        }

        public static Guid CreateGuidFromAssetPath(string assetPath)
		{
			if (string.IsNullOrEmpty(assetPath))
				throw new ArgumentNullException("assetPath cannot be null nor empty.");

			byte[] hashBytes = SHA256.HashData(Encoding.UTF8.GetBytes(assetPath));

			// Take the first 16 bytes of the hash to create a GUID
			byte[] result = new byte[16];
			Array.Copy(hashBytes, result, Math.Min(hashBytes.Length, 16));

			return new Guid(result);
		}

		internal void Initialize()
        {
            TextureAsset missingTexture = new("Content\\Empty.png");
            MissingTextureGuid = missingTexture.Guid;
            MissingTextureAssetRef = new(missingTexture.Guid);
        }

        public bool TryAddAsset(GameAsset asset)
        {
            if (asset.Guid == Guid.Empty)
            {
                asset.Guid = new();
            }

            return AllAssets.TryAdd(asset.Guid, asset);
        }

        public T GetAsset<T>(Guid id) where T : GameAsset
        {
            if (TryGetAsset<T>(id) is T asset)
            {
                return asset;
            }

            if (typeof(T) == typeof(TextureAsset))
            {
                if (TryGetAsset<T>(MissingTextureGuid) is T missingImageAsset)
                {
                    return missingImageAsset;
                }
            }

            throw new ArgumentException($"Unable to find the asset of type {typeof(T).Name} with id: {id} in database.");
        }

        public GameAsset GetAsset(Guid id)
        {
            if (TryGetAsset(id) is GameAsset asset)
            {
                return asset;
            }

            throw new ArgumentException($"Unable to find the asset with id: {id} in database.");
        }

        public GameAsset? TryGetAsset(Guid id)
        {
            if (AllAssets.TryGetValue(id, out GameAsset? asset))
            {
                return asset;
            }

            return default;
        }

        public T? TryGetAsset<T>(Guid id) where T : GameAsset
        {
            if (TryGetAsset(id) is T asset)
            {
                return asset;
            }

            return default;
        }

        public T GetOrCreateAsset<T>(string path) where T : GameAsset
        {
            if (TryGetAsset<T>(CreateGuidFromAssetPath(path)) is T asset)
            {
                return asset;
            }

			if ((asset = Activator.CreateInstance<T>()) is not null)
            {
                typeof(T).GetConstructor([typeof(string)])?.Invoke(asset, [path]);
                return asset;
            }

            throw new Exception($"Unable to create {typeof(T).Name} from {path}");
        }
    }
}
