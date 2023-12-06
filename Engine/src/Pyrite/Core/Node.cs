using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pyrite.Core
{
    public abstract class Node
    {
        public string Name = "Node";
        public ObjectNode? Parent;

        public bool IsOrphan => Parent == null;

        public event EventHandler<ObjectNode?>? OnParentChanged;

        // Node life-cycle events
        public event EventHandler? OnActivated;
        public event EventHandler? OnDeactivated;
        public event EventHandler? OnInitialized;
        public event EventHandler? OnDestroyed;

        public virtual void Activate() => OnActivated?.Invoke(this, EventArgs.Empty);
        public virtual void Deactivate() => OnDeactivated?.Invoke(this, EventArgs.Empty);

        /// <summary>
        /// Where you initialize every variables needed for the object 
        /// </summary>
        public virtual void Initialize() => OnInitialized?.Invoke(this, EventArgs.Empty);

        /// <summary>
        /// Destroy the object from the node tree.
        /// You need to call base.Destroy() before setting the object to null
        /// </summary>
        public virtual void Destroy() => OnDestroyed?.Invoke(this, EventArgs.Empty);

        /// <summary>
        /// Called every frame.
        /// </summary>
        public virtual void Update() { }

        /// <summary>
        /// Called every fixed tick.
        /// </summary>
        public virtual void FixedUpdate() { }

        /// <summary>
        /// Change the parent node of a node.
        /// </summary>
        /// <param name="parent">New parent of the node.</param>
        public virtual void SetParent(ObjectNode? parent)
        {
            Parent = parent;
            OnParentChanged?.Invoke(this, parent);
        }
    }
}
