//namespace Pyrite.Core.Graphics
//{
//    public enum BlendStyle
//    {
//        Normal,
//        Wash,
//        Color
//    }

//    public enum OutlineStyle
//    {
//        Full,
//        Top,
//        None
//    }

//    public readonly struct DrawInfo
//    {
//        public static DrawInfo Default => new();

//        /// <summary>
//        /// The origin of the image. From 0 to 1. Vector2Helper.Center is the center.
//        /// </summary>
//        public Vector2 Origin { get; init; } = Vector2.Zero;

//        /// <summary>
//        /// An offset to draw this image. In pixels
//        /// </summary>
//        public Vector2 Offset { get; init; } = Vector2.Zero;

//        /// <summary>
//        /// In degrees.
//        /// </summary>
//        public float Rotation { get; init; } = 0;
//        public Vector2 Scale { get; init; } = Vector2.One;
//        public Color Color { get; init; } = Color.White;
//        public float Sort { get; init; } = 0.5f;

//        public OutlineStyle OutlineStyle { get; init; } = OutlineStyle.Full;
//        public Color? Outline { get; init; } = null;
//        public Color? Shadow { get; init; } = null;
//        public bool Debug { get; init; } = false;

//        public BlendStyle BlendMode { get; init; } = BlendStyle.Normal;
//        public bool FlippedHorizontal { get; init; } = false;

//        public Rectangle Clip { get; init; } = Rectangle.Empty;
//        public DrawInfo()
//        {
//        }
//        public DrawInfo(Color color, float sort)
//        {
//            Color = color;
//            Sort = sort;
//        }

//        public DrawInfo(float sort)
//        {
//            Sort = sort;
//        }

//        public readonly Microsoft.Xna.Framework.Vector3 GetBlendMode()
//        {
//            return BlendMode switch
//            {
//                BlendStyle.Normal => new(1, 0, 0),
//                BlendStyle.Wash => new(0, 1, 0),
//                BlendStyle.Color => new(0, 0, 1),
//                _ => throw new Exception("Blend mode not supported!"),
//            };
//        }

//        internal DrawInfo WithScale(Vector2 size)
//        {
//            return new()
//            {
//                Origin = Origin,
//                Rotation = Rotation,
//                Color = Color,
//                Sort = Sort,
//                Scale = Scale * size,
//                Offset = Offset,
//                Shadow = Shadow,
//                Outline = Outline,
//                BlendMode = BlendMode,
//                FlippedHorizontal = FlippedHorizontal,
//                Debug = Debug
//            };
//        }

//        internal DrawInfo WithOffset(Vector2 offset)
//        {
//            return new()
//            {
//                Rotation = Rotation,
//                Color = Color,
//                Sort = Sort,
//                Scale = Scale,
//                Origin = Origin,
//                Offset = offset,
//                Shadow = Shadow,
//                Outline = Outline,
//                BlendMode = BlendMode,
//                FlippedHorizontal = FlippedHorizontal,
//                Debug = Debug
//            };
//        }

//        public DrawInfo WithSort(float sort) => new()
//        {
//            Rotation = Rotation,
//            Color = Color,
//            Sort = sort,
//            Scale = Scale,
//            Origin = Origin,
//            Offset = Offset,
//            Shadow = Shadow,
//            Outline = Outline,
//            BlendMode = BlendMode,
//            FlippedHorizontal = FlippedHorizontal,
//            Debug = Debug
//        };
//    }
//}
