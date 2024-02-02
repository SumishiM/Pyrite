using Silk.NET.OpenGL;

namespace Pyrite.Core.Graphics
{
    public static class Graphics
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
