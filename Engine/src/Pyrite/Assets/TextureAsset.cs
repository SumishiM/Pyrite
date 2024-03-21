using Pyrite.Core.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pyrite.Assets
{
    public class TextureAsset : GameAsset
    {
        Texture Texture { get; set; }

        public TextureAsset(string path, Texture texture) : base(path)
        {
            Texture = texture;
        }

    }
}
