using Ignite;
using Ignite.Systems;
using Pyrite.Components;
using Pyrite.Core.Graphics;
using Pyrite.Core;
using Pyrite.Utils;

namespace ODEs
{
    public class ParticleSpawner : IUpdateSystem
    {
        private float _spawnCooldown = 0.025f;
        private float _currentCooldown = 0f;
        private Random _random = new();

        public void Update(Context context)
        {
            for (int i = 0; i < 5; i++)
            {

                Node particle =
                    Node.CreateBuilder(SceneManager.CurrentScene.World, $"Particle")
                        .AddComponent<ParticleComponent>(new(50f))
                        .AddComponent<LifeTimeComponent>(new(25f))
                        .AddComponent<SpriteComponent>(new("Content\\Particle.png"));

                particle.GetComponent<TransformComponent>().Position =
                    new(400 + 100 * ((float)_random.NextDouble() * 2f - 1f), 300 + 100 * ((float)_random.NextDouble() * 2f - 1f));

                particle.GetComponent<TransformComponent>().Scale = new(2, 2);
            
            }
        }
    }
}
