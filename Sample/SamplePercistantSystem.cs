using Ignite.Systems;
using Pyrite.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample
{
    [PercistentSystem]
    public readonly struct SamplePercistantSystem : IStartSystem
    
    {
        public void Start(Context context)
        {
            Console.WriteLine("Percistant system started");
        }
    }
}
