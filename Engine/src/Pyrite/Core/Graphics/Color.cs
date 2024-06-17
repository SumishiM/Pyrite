using System.Globalization;
using System.Reflection;
using Pyrite.Utils;

namespace Pyrite.Core.Graphics
{
    public struct Color : IEquatable<Color>
    {
        public float R = 0f;
        public float G = 0f;
        public float B = 0f;
        public float A = 0f;

        public Color(float r, float g, float b, float a = 1f)
        {
            R = r;
            G = g;
            B = b;
            A = a;
        }

        public static Color From256(byte r, byte g, byte b, byte a)
            => new(r / 256f, g / 256f, b / 256f, a / 256f);

            
        /// <summary>
        /// Converts the murder color <param ref="c"/> into an XNA color.
        /// </summary>
        public static implicit operator Microsoft.Xna.Framework.Color(Color c) => new(c.R, c.G, c.B, c.A);
        /// <summary>
        /// Converts the XNA color <param ref="c"/> into a murder color.
        /// </summary>
        public static implicit operator Color(Microsoft.Xna.Framework.Color c) => new(c.R / 255f, c.G / 255f, c.B / 255f, c.A / 255f);
        /// <summary>
        /// Converts the Vector4 into a murder color (X = R, Y = G, Z = B, W = A).
        /// </summary>
        public static implicit operator Color(System.Numerics.Vector4 c) => new(c.X, c.Y, c.Z, c.W);
        /// <summary>
        /// Multiplies all values of the color by the float <param ref="r"/>
        /// </summary>
        public static Color operator *(Color l, float r) => new(l.R * r, l.G * r, l.B * r, l.A * r);

        /// <summary>
        /// Multiplies each color value in color <param ref="l"/> by their correspondent counterpart in color <param ref="r"/>
        /// </summary>
        public static Color operator *(Color l, Color r) => new(l.R * r.R, l.G * r.G, l.B * r.B, l.A * r.A);
        /// <summary>
        /// Checks if two colors are equal.
        /// </summary>
        public bool Equals(Color other) => R == other.R && G == other.G && B == other.B && A == other.A;

        /// <inheritdoc cref="Object"/>
        public override bool Equals(object? obj) => obj is Color other && Equals(other);

        /// <inheritdoc cref="Object"/>
        public override int GetHashCode() => HashCode.Combine(R, G, B, A);

        /// <inheritdoc cref="Object"/>
        public static bool operator ==(Color lhs, Color rhs) => lhs.Equals(rhs);

        /// <inheritdoc cref="Object"/>
        public static bool operator !=(Color lhs, Color rhs) => !lhs.Equals(rhs);
        /// <inheritdoc cref="Object"/>
        public override string ToString()
        {
            return string.Format(CultureInfo.InvariantCulture, "Color({0}, {1}, {2}, {3})", R, G, B, A);
        }
        /// <summary>
        /// Multiplies the R, G and B values of this color by the Alpha value.
        /// </summary>
        public Color Premultiply()
        {
            return new Color(
                (R * A),
                (G * A),
                (B * A),
                A
            );
        }

        /// <summary>
        /// Finds a color that is in the point <paramref name="factor"/> between <paramref name="a"/> and <paramref name="b"/>.
        /// </summary>
        public static Color Lerp(Color a, Color b, float factor)
            => new(
                Calculator.Lerp(a.R, b.R, factor),
                Calculator.Lerp(a.G, b.G, factor),
                Calculator.Lerp(a.B, b.B, factor),
                Calculator.Lerp(a.A, b.A, factor)
            );

        public static Color LerpSmooth(Color a, Color b, float deltaTime, float halfLife)
            => new(
                Calculator.LerpSmooth(a.R, b.R, deltaTime, halfLife),
                Calculator.LerpSmooth(a.G, b.G, deltaTime, halfLife),
                Calculator.LerpSmooth(a.B, b.B, deltaTime, halfLife),
                Calculator.LerpSmooth(a.A, b.A, deltaTime, halfLife)
            );
    }
}