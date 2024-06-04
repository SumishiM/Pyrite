using Ignite.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODEs
{
    
    public class LifeTimeComponent : IComponent
    {
        public float LifeTime;

        public LifeTimeComponent() { }
        public LifeTimeComponent(float lifeTime)
        {
            LifeTime = lifeTime;
        }
    }
}
