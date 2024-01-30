using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pyrite.Utils
{
    public static class MathHelper
    {
        public static float ToRadians ( this float degrees ) => MathF.PI / 180f * degrees;
    }
}
