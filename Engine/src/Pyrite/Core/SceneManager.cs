using Ignite.Attributes;
using Pyrite.Core.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pyrite.Core
{
    public static class SceneManager
    {
        private static Scene _defaultScene => new("Sample Scene");
        internal static Scene ConfiguratedDefaultScene = _defaultScene;
        public static Scene CurrentScene = ConfiguratedDefaultScene;

        public static Action<Scene>? OnSceneChanged;
        public static Action<Scene>? OnScenePaused;
        public static Action<Scene>? OnSceneResumed;

        public static bool LoadScene(ref Scene scene)
        {
            CurrentScene.Exit();
            Camera._main = null;
            
            CurrentScene = scene;
            CurrentScene.Start();
            OnSceneChanged?.Invoke(CurrentScene);
            return true;
        }

        public static void Pause()
        {
            CurrentScene.Pause();
            OnScenePaused?.Invoke(CurrentScene);
        }

        public static void Resume()
        {
            CurrentScene.Resume();
            OnSceneResumed?.Invoke(CurrentScene);
        }
    }
}
