
using Pyrite.Core.Geometry;
using System.Collections.Immutable;

namespace Pyrite.Core.Tilemap
{
    public class Tilemap
    {
        public static byte ChunkSize = 16;
        public byte TileSize = 16;

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
                    if (x < 0 || x >= ChunkSize || y < 0 || y >= ChunkSize)
                        throw new IndexOutOfRangeException();
                    return ref _tiles[x, y];
                }
            }

        }

        internal List<Chunk>? BuildingChunks;
        private ImmutableArray<Chunk> _cachedChunks;


        public Tilemap Build()
        {
            _cachedChunks = [.. BuildingChunks];
            BuildingChunks?.Clear();

            return this;
        }
    }
}
