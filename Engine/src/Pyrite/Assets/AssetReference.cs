namespace Pyrite.Assets
{
    /// <summary>
    /// Point a <see cref="GameAsset"/> in the <see cref="AssetDatabase"/>
    /// </summary>
    /// <typeparam name="T">Type of <see cref="GameAsset"/></typeparam>
    public class AssetReference<T>(Guid guid) where T : GameAsset
    {
        /// <summary>
        /// Empty asset reference with no <see cref="Guid"/>
        /// </summary>
        /// <returns></returns>
        public static AssetReference<T> Empty => new(Guid.Empty);

        /// <summary>
        /// Whether the asset is empty or not
        /// </summary>
        public bool HasValue => Guid != Guid.Empty;

        /// <summary>
        /// Asset unique ID
        /// </summary>
        public readonly Guid Guid = guid;

        /// <summary>
        /// Asset pointed by the reference
        /// </summary>
        /// <typeparam name="T">Type of the <see cref="GameAsset"/> referenced</typeparam>
        /// <returns>The <see cref="GameAsset"/> pointed by the reference</returns>
        public T Asset => Game.Data.GetAsset<T>(Guid);

        /// <summary>
        /// Try get the asset from the reference
        /// </summary>
        /// <typeparam name="T">Type of the <see cref="GameAsset"/> referenced</typeparam>
        /// <returns>The <see cref="GameAsset"/> pointed by the reference or null</returns>
        public T? TryAsset => Game.Data.TryGetAsset<T>(Guid);
    }
}
