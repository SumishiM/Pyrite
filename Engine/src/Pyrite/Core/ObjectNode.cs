using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;

namespace Pyrite.Core
{
    /// <summary>
    /// Main class for instantiated objects in a scene. Can have children and <see cref="Component"/>s.
    /// </summary>
    public class ObjectNode : Node
    {
        public Transform Transform { get; protected set; }
        public Transform WorldTransform { get; protected set; }

        public IList<Node> Children;
        public event EventHandler<Node>? OnChildAdded;
        public event EventHandler<Node>? OnChildRemoved;

        public IList<Component> Components;
        public event EventHandler<Component>? OnComponentAdded;
        public event EventHandler<Component>? OnComponentRemoved;

        public ObjectNode()
        {
            Children = new List<Node>();
            Components = new List<Component>();
            Transform = Transform.Default;
            WorldTransform = Transform.Default;
        }

        /// <inheritdoc/>
        /// <remarks>
        /// Components are updated first, then children.
        /// </remarks>
        public override void Update()
        {
            foreach (Component component in Components) 
                component.Update();

            foreach (Node child in Children)
                child.Update();
        }

        /// <inheritdoc/>
        /// <remarks>
        /// Remove itself from the old parent children list and is added to the new parent children list.
        /// </remarks>
        /// <param name="parent">New parent of the node.</param>
        public override void SetParent(ObjectNode? parent)
        {
            Parent?.RemoveChild(this);
            parent?.AddChild(parent);
            base.SetParent(parent);
        }

        /// <summary>
        /// Add a child node to this node
        /// </summary>
        /// <param name="child">Child node</param>
        public void AddChild([DisallowNull] Node child)
        {
            if (Children.Contains(child))
                return; // todo: Add A console log here when logger is done

            child.SetParent(this);
            Children.Add(child);
            OnChildAdded?.Invoke(this, child);
        }

        /// <summary>
        /// Remove a child node from this node
        /// </summary>
        /// <param name="child">Node to remove</param>
        public void RemoveChild([DisallowNull] Node child)
        {
            if (!Children.Contains(child))
                return; // todo: Add A console log here when logger is done

            Children.Remove(child);
            child.SetParent(null);

            OnChildRemoved?.Invoke(this, child);
        }

        /// <summary>
        /// Remove a child node from this node using it's name
        /// </summary>
        /// <param name="childName">Name of the node to remove</param>
        public void RemoveChild(string childName)
        {
            Node? child = Children.FirstOrDefault(x => x.Name == childName);
            if (child == null)
                return; // todo: Add A console log here when logger is done

            RemoveChild(child);
        }

        /// <summary>
        /// Remove a child node from this node using it's Id
        /// </summary>
        /// <param name="childId">Id of the node to remove</param>
        public void RemoveChild(int childId)
        {
            if( Children.Count < childId)
                return; // todo: Add A console log here when logger is done

            RemoveChild(Children[childId]);
        }

        /// <summary>
        /// Add a child node to this node
        /// </summary>
        /// <param name="child">Child node</param>
        public void AddComponent([DisallowNull] Component component)
        {
            if (Components.Contains(component))
                return; // todo: Add A console log here when logger is done

            component.SetParent(this);
            Components.Add(component);
            OnComponentAdded?.Invoke(this, component);
        }

        /// <summary>
        /// Remove a child node from this node
        /// </summary>
        /// <param name="child">Node to remove</param>
        public void RemoveComponent([DisallowNull] Component child)
        {
            if (!Components.Contains(child))
                return; // todo: Add A console log here when logger is done

            Components.Remove(child);
            child.SetParent(null);

            OnComponentRemoved?.Invoke(this, child);
        }

        /// <summary>
        /// Remove a child node from this node using it's name
        /// </summary>
        /// <param name="childName">Name of the node to remove</param>
        public void RemoveComponent(string componentName)
        {
            Component? component = Components.FirstOrDefault(x => x.Name == componentName);

            if (component == null)
                return; // todo: Add A console log here when logger is done

            RemoveComponent(component);
        }

        /// <summary>
        /// Remove a child node from this node using it's Id
        /// </summary>
        /// <param name="childId">Id of the node to remove</param>
        public void RemoveComponent(int componentId)
        {
            if (Children.Count < componentId)
                return; // todo: Add A console log here when logger is done

            RemoveComponent(Components[componentId]);
        }

        /// <summary>
        /// Get the first component of type T
        /// </summary>
        /// <typeparam name="T">Type of the component</typeparam>
        /// <returns>A Component of type T</returns>
        public T? GetComponent<T>() where T : Component
        {
            Type searchedType = typeof(T);
            return Components.FirstOrDefault(c => c.GetType() == searchedType) as T;
        }

        /// <summary>
        /// Get the first component of type T
        /// </summary>
        /// <typeparam name="T">Type of the component</typeparam>
        /// <returns>A Component of type T</returns>
        public T? GetComponent<T>(string componentName) where T : Component
        {
            Type searchedType = typeof(T);
            return Components.FirstOrDefault(c => c.GetType() == searchedType && c.Name == componentName) as T;
        }

        /// <summary>
        /// Find every components of a given type
        /// </summary>
        /// <typeparam name="T">Type of the components</typeparam>
        /// <returns>A Collection of components of type T</returns>
        public IEnumerable<T>? GetComponents<T>() where T : Component
        {
            Type searchedType = typeof(T);
            return Components.Where(c => c.GetType() == searchedType) as IEnumerable<T>;
        }
    }
}
