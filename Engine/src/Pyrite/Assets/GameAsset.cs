using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pyrite.Assets
{
    public class GameAsset
    {

        public string? Name { get; set; }
        public string? Path { get; set; }
        public string? Extention { get; set; }

        public Guid Guid { get; internal set; }


        public GameAsset(string path)
        {
            Name = System.IO.Path.GetFileNameWithoutExtension(path.Split('\\', '/').Last());
            Extention = System.IO.Path.GetExtension(path.Split('\\', '/').Last());
            Path = path.Replace(Name + Extention, string.Empty);
        }
    }
}
