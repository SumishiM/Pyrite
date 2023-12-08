using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pyrite.Core.Inputs
{
    internal interface IVirtualInput
    {
        public void Update(InputState state);
    }
}
