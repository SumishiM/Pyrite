﻿using Ignite.Attributes;
using Ignite.Systems;
using Pyrite.Attributes;
using Pyrite.Components;
using Pyrite.Core;
using Pyrite.Utils;

namespace Sandbox
{
    [FilterComponent(typeof(SpinComponent))]
    public struct SpinSystem : IFixedUpdateSystem
    {
        public void FixedUpdate( Context context )
        {
            foreach ( var node in context.Nodes )
            {
                Transform transform = node.GetComponent<TransformComponent>();
                transform.Rotation += node.GetComponent<SpinComponent>().SpinSpeed * Time.FixedDeltaTime;
            }
        }

        public void Dispose ()
        {
        }
    }

    [PercistentSystem]
    public struct PermaSystem : IStartSystem
    {
        public void Dispose()
        {
        }

        public void Start(Context context)
        {
        }
    }
}