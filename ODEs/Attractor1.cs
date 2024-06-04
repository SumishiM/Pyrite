using Ignite.Attributes;
using Ignite.Systems;
using Pyrite.Components;
using Pyrite.Core.Geometry;
using Pyrite.Utils;

namespace ODEs
{
    [FilterComponent(typeof(ParticleComponent))]
    public struct Attractor1 : IUpdateSystem
    {
        public float Alpha = 0.1f;

        public Attractor1() { }
        public Attractor1(float alpha) 
        {
            Alpha = alpha;
        }

        public readonly void Update(Context context)
        {
            foreach (var (particle, transform) in context.Get<ParticleComponent, TransformComponent>())
            {
                transform.Position += 
                    new Vector2(
                        MathF.Cos(Alpha * transform.Position.Y), 
                        MathF.Sin(Alpha * transform.Position.X)
                    ) * particle.Speed * Time.DeltaTime;
            }
        }
    }
}
