using Pyrite.Assets;

namespace Pyrite.Core.Graphics
{
    public readonly struct PyriteTexture
    {
        private readonly Guid _textureGuid;

        public PyriteTexture(string path)
        {
            _textureGuid = AssetDatabase.HashTexturePath(path);
        }

        internal PyriteTexture(Guid guid)
        {
            _textureGuid = guid;
        }

        public void PreLoad()
        {
            if (_textureGuid != Guid.Empty)
            {
                // fetch texture in asset database
            }
        }
    }
}