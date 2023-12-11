using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pyrite.Core
{
    public class ComponentPriority
    {
        public const int INPUT = 10;
        public const int DEFAULT = 100;



        // this class should never be an object
        protected ComponentPriority() { }
    }

    public abstract class Component : Node
    {
        public virtual int Priority => ComponentPriority.DEFAULT;

        /// <inheritdoc/>
        /// <remarks>
        /// Remove itself from the old parent component list and is added to the new parent component list.
        /// </remarks>
        /// <param name="parent">Parent node for the component.</param>
        public override void SetParent(ObjectNode? node)
        {
            Parent?.RemoveComponent(this);
            node?.AddComponent(this);

            base.SetParent(node);
        }
    }
}
