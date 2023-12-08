using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pyrite.Core.Scenes
{
    public class Scene : IDisposable
    {
        public RootNode Root;

        public Scene()
        {
            Root = new RootNode();
        }

        public void Dispose()
        {
            Root.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
