using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pyrite.Core.Inputs
{
    public class PlayerInput : Component
    {
        public override int Priority => ComponentPriority.INPUT;



        public InputContextMapping? IMC;
    }
}
