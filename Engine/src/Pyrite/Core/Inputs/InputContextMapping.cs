using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pyrite.Core.Inputs
{
    public class InputContextMapping
    {
        IDictionary<InputAction, HashSet<ButtonBinding>> _buttonsMapping;
        IDictionary<InputAction, HashSet<AxisBinding>> _axisMapping;
    }
}
