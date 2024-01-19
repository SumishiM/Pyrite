namespace Pyrite.Assets
{
    public class AssetDatabase
    {
        private static AssetDatabase _db;

        static AssetDatabase()
        {
            _db = new AssetDatabase();
            _allAssets = [];
            _database = [];
        }

        public AssetDatabase () 
        {
            _db = this;
        }

        private static readonly Dictionary<Guid, GameAsset> _allAssets;
        private static readonly Dictionary<Type, HashSet<Guid>> _database;

        public static bool HasAsset<T>(Guid guid) where T : GameAsset =>
            _database.TryGetValue(typeof(T), out HashSet<Guid>? ids) && ids.Contains(guid);

        public static T GetAsset<T>(Guid key) where T : GameAsset
        {
            if (TryGetAsset<T>(key) is T asset)
            {
                return asset;
            }

            throw new ArgumentException($"Unable to find the asset of type {typeof(T).Name} with id: {key} in database.");
        }

        public static T? TryGetAsset<T>(Guid key) where T : GameAsset
        {
            if (_allAssets.TryGetValue(key, out GameAsset? asset))
            {
                return asset as T;
            }

            return default;
        }
    }
}
