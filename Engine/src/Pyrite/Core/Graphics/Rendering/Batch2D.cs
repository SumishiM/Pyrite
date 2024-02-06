using Pyrite.Core.Graphics.Rendering.OpenGL;

namespace Pyrite.Core.Graphics.Rendering
{
    // WIP
    public class Batch2D
    {

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
        }
    }
}
