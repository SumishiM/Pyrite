using Pyrite.Core.Graphics;
using Pyrite.Core.Inputs;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Pyrite.Assets
{
    public class AssetDatabase
    {
        internal Dictionary<Type, HashSet<Guid>> Lookup = new()
        {
            {typeof(Texture), []}
        };

        internal Dictionary<Guid, GameAsset> AllAssets = [];
        internal Dictionary<Guid, Texture> UniqueTextures = [];
        internal Guid MissingTextureGuid;


        public AssetDatabase()
        {
        }

        internal void Initialize()
        {
            Texture.Create("Content\\Empty.png");
            MissingTextureGuid = HashTexturePath("Content\\Empty.png");
        }

        public bool TryGetTexture(string path, [NotNullWhen(true)] out Texture? texture)
        {
            texture = null;
            if (UniqueTextures.TryGetValue(HashTexturePath(path), out Texture? value))
            {
                texture = value;
                return true;
            }
            return false;
        }

        internal void AddTexture(string path, Texture texture)
        {
            Guid guid = HashTexturePath(path);
            AllAssets.Add(guid, new GameAsset(path));
            UniqueTextures.TryAdd(guid, texture);
            Lookup[typeof(Texture)].Add(guid);
        }


        private static Guid HashTexturePath(string path)
        {
            return new(MD5.HashData(Encoding.UTF8.GetBytes(path)));
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
    }
}
