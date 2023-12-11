namespace Pyrite.Core.Graphics
{
    internal class LookupSprite
    {
        Texture2D Source;
        Texture2D Lookup;
        Texture2D? Remap = null;

        public LookupSprite(Texture2D source, Texture2D lookup, Texture2D? remap = null)
        {
            Source = source;
            Lookup = lookup;
            Remap = remap;
        }
    }
}
