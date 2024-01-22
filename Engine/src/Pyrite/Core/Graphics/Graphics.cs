using Silk.NET.OpenGL;

namespace Pyrite.Core.Graphics
{
    public class Graphics
    {
        private static GL? _gl = null;
        internal static GL Gl
        {
            get
            {
                _gl ??= GL.GetApi(Game.Window?.Native());
                return _gl;
            }
        }

    }
}
