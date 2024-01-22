﻿using Silk.NET.Core;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace Pyrite.Core.Graphics
{
    public sealed class Icon
    {
        public int Width;
        public int Height;

        public RawImage Raw;

        private static string PyriteIconPath = "Content\\PyriteIcon128.png";

        public Icon(string path)
        {
            using var image = new Bitmap(path);
            using var resized = (Bitmap)image.GetThumbnailImage(256, 256, () => false, IntPtr.Zero);
            for (var y = 0; y < resized.Height; y++)
            {
                for (var x = 0; x < resized.Width; x++)
                {
                    var pixel = resized.GetPixel(x, y);
                    var a = pixel.A;
                    var r = pixel.R;
                    var g = pixel.G;
                    var b = pixel.B;
                    resized.SetPixel(x, y, Color.FromArgb(a, b, g, r));
                }
            }
            var rect = new Rectangle(0, 0, resized.Width, resized.Height);
            var data = resized.LockBits(rect, ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            var length = data.Stride * data.Height;
            var bytes = new byte[length];
            Marshal.Copy(data.Scan0, bytes, 0, length);
            resized.UnlockBits(data);

            Width = resized.Width;
            Height = resized.Height;
            Raw = new(Width, Height, bytes);
        }

        public static Icon Default => new(PyriteIconPath);
    }
}
