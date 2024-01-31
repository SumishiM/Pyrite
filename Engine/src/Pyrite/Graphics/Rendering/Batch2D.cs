using Pyrite.Core;
using Pyrite.Graphics.Shaders;
using Silk.NET.Vulkan;

namespace Pyrite.Graphics.Rendering
{
    // WIP
    public class Batch2D
    {
        List<Sprite> Sprites;

        Dictionary<int, Shader> Shaders;
        Dictionary<int, Texture> Textures;

        public struct RenderData
        {
            int Shader;
            int Texture;
            Transform Transform;
        }

        public Dictionary<int, HashSet<int>> ShaderToTextures;
        public Dictionary<int, RenderData> RenderDatas;


        public Batch2D()
        {
            Sprites = new List<Sprite>();
        }
    }
}
