
using Pyrite.Core.Geometry;
using System.Collections.Immutable;

namespace Pyrite.Core.Tilemap
{
    public class Tilemap
    {
        public static byte ChunkSize = 16;
        public byte TileSize = 16;

        /// <summary>
        /// A segmented part of a <see cref="Tilemap"/> containing <see cref="Tile"/>s on the map.
        /// </summary>
        public struct Chunk
        {
            /// <summary>
            /// Offset of the chunk on it's <see cref="Tilemap"/>
            /// </summary>
            public Point Offset { get; set; } = Point.Zero;

            private readonly Tile[,] _tiles;

            /// <summary>
            /// <see cref="Tile"/> grid of the chunk
            /// </summary>
            public readonly Tile[,] Tiles => _tiles;

            /// <summary>
            /// Create an empty chunk with every <see cref="Tile"/>s with a value of -1
            /// </summary>
            public Chunk()
            {
                _tiles = new Tile[ChunkSize, ChunkSize];
            }

            /// <summary>
            /// Create a chunk from a grid of <see cref="Tile"/>
            /// </summary>
            public Chunk(Tile[,] tiles)
            {
                if (tiles.Length != ChunkSize * ChunkSize)
                    throw new Exception("Creating a Tilemap chunk with the wrong size !");

                _tiles = tiles;
            }


            /// <summary>
            /// Get a <see cref="Tile"/> reference from it's local coordonates in the chunk
            /// </summary>
            /// <param name="x">Local X coordonate in the chunk</param>
            /// <param name="y">Local Y coordonate in the chunk</param>
            /// <returns>The <see cref="Tile"/> index, -1 if empty.</returns>
            /// <exception cref="IndexOutOfRangeException"></exception>
            public readonly ref Tile this[int x, int y]
            {
                get
                {
#if DEBUG
                    if (x < 0 || x >= ChunkSize)
                        throw new IndexOutOfRangeException($"X value must be between a [0, {ChunkSize - 1}] interval : X is {x}");
                    if (y < 0 || y >= ChunkSize)
                        throw new IndexOutOfRangeException($"Y value must be between a [0, {ChunkSize - 1}] interval :Y is {x}");
#endif
                    return ref _tiles[x, y];
                }
            }
        }

        // one layer thing
        internal Dictionary<Point, Chunk>? BuildingChunks;
        // cached chunks
        private ImmutableArray<Chunk> _cachedChunks;

        /// <summary>
        /// Build the <see cref="Tilemap"/>, making it size immutable.
        /// </summary>
        /// <returns>The built <see cref="Tilemap"/> or null if there is no <see cref="Tile"/> placed</returns>
        public Tilemap? Build()
        {
            _cachedChunks = [.. BuildingChunks?.Values];
            BuildingChunks?.Clear();

            return this;
        }
    }
}
