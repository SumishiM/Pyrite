﻿using Silk.NET.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pyrite.Core.Inputs
{
    public interface IVirtualInput
    {
        public void Update(ICollection<IInputDevice> devices);
    }
}