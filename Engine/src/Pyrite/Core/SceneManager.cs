using Ignite.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pyrite.Core
{
    public static class SceneManager
    {
        private static Scene _defaultScene => new Scene("Sample Scene");
        internal static Scene ConfiguratedDefaultScene = _defaultScene;
        public static Scene CurrentScene { get; private set; } = ConfiguratedDefaultScene; 

        public static bool LoadScene(Scene scene)
        {
            if (CurrentScene != null)
            {
#if DEBUG
                Console.WriteLine($"Unload scene : {CurrentScene.Name}");
#endif
                CurrentScene.Exit();
            }
            CurrentScene = scene;
#if DEBUG
            Console.WriteLine($"Load scene : {CurrentScene.Name}");
#endif
            CurrentScene.Start();
            return true;
        }

        public static void Pause()
        {
            CurrentScene?.Pause();
        }

        public static void Resume()
        {
            CurrentScene?.Resume();
        }
    }
}
