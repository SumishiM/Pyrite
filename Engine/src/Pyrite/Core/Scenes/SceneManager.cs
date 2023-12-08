using System.Collections.Immutable;

namespace Pyrite.Core.Scenes
{
    public class SceneManager
    {
        public static Scene? Current;

        public static ImmutableDictionary<string, Scene> SceneMap = ImmutableDictionary.Create<string, Scene>();

        public SceneManager() { }

        public Scene? LoadScene(string sceneName)
        {
            return default;
        }
    }
}
