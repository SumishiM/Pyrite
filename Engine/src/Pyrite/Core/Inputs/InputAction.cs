using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pyrite.Core.Inputs
{
    public class InputAction
    {
        IList<Action<CallbackContext>> _listeners;

        ReadOnlyCollection<VirtualInput> _inputs;



        public struct CallbackContext
        {
            public InputAction Action { get; init; }
            public Type ValueType {get; init;}

        }
    }
}
