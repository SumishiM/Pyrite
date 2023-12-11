namespace Pyrite.Core
{
    /// <summary>
    /// Main class for the engine managed objects 
    /// </summary>
    public abstract class Node : ICloneable, IDisposable, IEquatable<Node>
    {
        public string Name = "Node";
        public ObjectNode? Parent;

        public uint UID { get; private set; } = 0;
        public bool IsActive { get; set; } = true;
        public bool IsOrphan => Parent == null;

        public event EventHandler<ObjectNode?>? OnParentChanged;

        // Node life-cycle events
        public event EventHandler? OnActivated;
        public event EventHandler? OnDeactivated;
        public event EventHandler? OnInitialized;
        public event EventHandler? OnDestroyed;

        public Node()
        {
            if (UID == 0)
                GenerateUID();
        }

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
        protected virtual void Destroy() => OnDestroyed?.Invoke(this, EventArgs.Empty);

        /// <summary>
        /// Called every frame.
        /// </summary>
        public virtual void Update() { }

        /// <summary>
        /// Change the parent node of a node.
        /// </summary>
        /// <param name="parent">New parent of the node.</param>
        public virtual void SetParent(ObjectNode? parent)
        {
            Parent = parent;
            OnParentChanged?.Invoke(this, parent);
        }

        public object Clone()
        {
            return MemberwiseClone();
        }

        private void GenerateUID()
        {
            UID = 0;
        }


        public void Dispose()
        {
            Destroy();
            GC.SuppressFinalize(this);
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as Node);
        }

        public bool Equals(Node? other)
        {
            return UID == other?.UID;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
