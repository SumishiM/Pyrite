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

        internal Dictionary<Guid, Texture> UniqueTextures = [];

        internal bool TryGetTexture(string path, [NotNullWhen(true)] out Texture? texture)
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
            UniqueTextures.TryAdd(HashTexturePath(path), texture);
        }


        private static Guid HashTexturePath(string path)
        {
            return new(MD5.HashData(Encoding.UTF8.GetBytes(path)));
        }
    }
}
