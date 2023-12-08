using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pyrite.Assets
{
    public class AssetReference<T> where T : GameAsset
    {
        public static AssetReference<T> Empty = new AssetReference<T>(Guid.Empty);

        public bool HasValue => Guid != Guid.Empty;
        public readonly Guid Guid;

        public AssetReference(Guid guid)
        {
            Guid = guid;
        }

        public T Asset => AssetDatabase.GetAsset<T>(Guid);
        public T? TryAsset => AssetDatabase.TryGetAsset<T>(Guid);
    }
}
