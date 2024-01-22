namespace Pyrite.Assets
{
    public class AssetReference<T>(Guid guid) where T : GameAsset
    {
        public static AssetReference<T> Empty => new(Guid.Empty);

        public bool HasValue => Guid != Guid.Empty;
        public readonly Guid Guid = guid;

        public T Asset => AssetDatabase.GetAsset<T>(Guid);
        public T? TryAsset => AssetDatabase.TryGetAsset<T>(Guid);
    }
}
