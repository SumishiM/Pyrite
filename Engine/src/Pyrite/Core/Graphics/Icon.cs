using Silk.NET.Core;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace Pyrite.Core.Graphics
{
    public sealed class Icon
    {
        /// <summary>
        /// Size of the icon
        /// </summary>
        public Geometry.Point Size;

        /// <summary>
        /// Raw icon data
        /// </summary>
        public RawImage Raw;

        /// <summary>
        /// Default Pyrite icon path
        /// </summary>
        private static string PyriteIconPath => "Content\\PyriteIcon128.png";

        /// <summary>
        /// Default Pyrite icon
        /// </summary>
        public static Icon Default => new(PyriteIconPath);

        /// <summary>
        /// Create an icon from an image file path
        /// </summary>
        public Icon(string path)
        {
            // load image
            using var img = Image.Load<Rgba32>(path);
            
            Size = new(img.Bounds.Width, img.Bounds.Height);

            // Copy image data
            // 4 is the size of the Rbga32 struct as bytes
            byte[] imageData = new byte[Size.X * Size.Y * 4];
            img.CopyPixelDataTo(imageData);

            // create Silk.NET raw image data
            Raw = new(Size.X, Size.Y, imageData);
        }

    }
}
