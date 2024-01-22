//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Pyrite.Core.Graphics
//{
//    public enum BatchMode
//    {

//    }

//    internal class Batch2D
//    {
//        public const int GameplayBatchId = 0;
//        public const int UIBatchId = 1;
//        public const int DebugBatchId = 2;

//        public string Name;

//        public GraphicsDevice GraphicsDevice { get; set; }
//        public readonly BatchMode BatchMode;
//        public readonly BlendState BlendState;
//        public readonly SamplerState SamplerState;
//        public readonly DepthStencilState DepthStencilState;
//        public readonly RasterizerState RasterizerState;
//        public Effect Effect;

//        private bool _followCamera = false;
//        public bool AutoHandleAlphaBlendedSprites;

//        public Batch2D(string name,
//            GraphicsDevice graphicsDevice,
//            Effect effect,
//            BatchMode batchMode,
//            BlendState blendState,
//            SamplerState samplerState,
//            DepthStencilState? depthStencilState = null,
//            RasterizerState? rasterizerState = null,
//            bool autoHandleAlphaBlendedSprites = false)
//            : this(
//                  name,
//                  graphicsDevice,
//                  false,
//                  effect,
//                  batchMode,
//                  blendState,
//                  samplerState,
//                  depthStencilState,
//                  rasterizerState,
//                  autoHandleAlphaBlendedSprites)
//        { }

//        public Batch2D(string name,
//        GraphicsDevice graphicsDevice,
//        bool followCamera,
//        Effect effect,
//        BatchMode batchMode,
//        BlendState blendState,
//        SamplerState samplerState,
//        DepthStencilState? depthStencilState = null,
//        RasterizerState? rasterizerState = null,
//        bool autoHandleAlphaBlendedSprites = false)
//        {
//            Name = name;

//            GraphicsDevice = graphicsDevice;
//            Effect = effect;
//            BatchMode = batchMode;
//            BlendState = blendState;
//            SamplerState = samplerState;
//            DepthStencilState = depthStencilState ?? DepthStencilState.None;
//            RasterizerState = rasterizerState ?? RasterizerState.CullNone;
//            _followCamera = followCamera;

//            AutoHandleAlphaBlendedSprites = autoHandleAlphaBlendedSprites;

//            if (AutoHandleAlphaBlendedSprites)
//            {
//                //_transparencyBatchItems = new SpriteBatchItem[_batchItems.Length / 2];
//                InitializeTransparencyItemsBuffers();
//            }

//            Initialize();
//        }

//        private void Initialize()
//        {
//            throw new NotImplementedException();
//        }

//        private void InitializeTransparencyItemsBuffers()
//        {
//            throw new NotImplementedException();
//        }
//    }
//}
