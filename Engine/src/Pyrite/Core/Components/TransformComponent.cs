﻿using Ignite.Components;

namespace Pyrite.Core.Components
{
    public class TransformComponent : IComponent
    {
        public Transform Transform { get; set; }
    
        public TransformComponent(Transform transform)
        {
            Transform = transform;
        }   

        public static implicit operator TransformComponent(Transform transform) => new (transform);
        public static explicit operator Transform(TransformComponent transform) => transform.Transform;
    }
}