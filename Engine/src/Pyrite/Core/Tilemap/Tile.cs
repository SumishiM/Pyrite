using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pyrite.Core.Tilemap
{
    public readonly struct Tile
    {
        public readonly int Index { get; init; } = -1;

        public Tile(int index)
        { 
            Index = index; 
        }
    }
}
