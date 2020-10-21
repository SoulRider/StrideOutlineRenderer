using Stride.Core;
using Stride.Core.Mathematics;
using Stride.Graphics;
using Stride.Rendering;
using Stride.Rendering.Compositing;

// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable once UnusedAutoPropertyAccessor.Global

namespace StrideOutlineRenderer.Renderers
{
    [Display("Outline Renderer")]
    public class OutlineRenderer : SceneRendererBase, IOutlineRenderer
    {
        public Texture InputRenderTexture { get; set; }
        public Texture OutputRenderTexture { get; set; }
        public float Scale { get; set; } = 1.005f;
        public Color Color
        {
            get => highlightColor;
            set
            {
                highlightColor = value;
                amplifiedColor = new Color4(
                    Color.R * ColorAmplifier, 
                    Color.G * ColorAmplifier,
                    Color.B * ColorAmplifier, 
                    ColorAmplifier);
            }
        }

        //public float OffsetVertical
        //{
        //    get => verticalOffset;
        //    set
        //    {
        //        verticalOffset = value;
        //        up = -Vector2.UnitY * verticalOffset;
        //        down = Vector2.UnitY * verticalOffset;
        //    }
        //}

        //public float OffsetHorizontal
        //{
        //    get => horizontalOffset;
        //    set
        //    {
        //        horizontalOffset = value;
        //        left = -Vector2.UnitX * horizontalOffset;
        //        right = Vector2.UnitX * horizontalOffset;
        //    }
        //}

        private SpriteBatch spriteBatch;
        //private Vector2 textureOffset;
        private float verticalOffset;
        private float horizontalOffset;

        private Color highlightColor;
        private Color4 amplifiedColor;

        //private Vector2 up;
        //private Vector2 down;
        //private Vector2 left;
        //private Vector2 right;

        private const float ColorAmplifier = 10f;

        public void ChangeColor(Color color)
        {
            Color = color;
        }

        protected override void InitializeCore()
        {
            base.InitializeCore();
            InitializeOutlineRenderer();
        }

        protected override void DrawCore(RenderContext context, RenderDrawContext drawContext)
        {
            if (InputRenderTexture == null || OutputRenderTexture == null)
                return;

            using (drawContext.PushRenderTargetsAndRestore())
            {
                var depthBuffer = PushScopedResource(context.Allocator.GetTemporaryTexture2D(OutputRenderTexture.ViewWidth, OutputRenderTexture.ViewHeight, drawContext.CommandList.DepthStencilBuffer.ViewFormat, TextureFlags.DepthStencil));
                drawContext.CommandList.SetRenderTargetAndViewport(depthBuffer, OutputRenderTexture);

                spriteBatch.Begin(drawContext.GraphicsContext, depthStencilState: DepthStencilStates.None);
                //spriteBatch.Draw(InputRenderTexture, up, amplifiedColor, 0, Vector2.Zero);
                //spriteBatch.Draw(InputRenderTexture, down, amplifiedColor, 0, Vector2.Zero);
                //spriteBatch.Draw(InputRenderTexture, left, amplifiedColor, 0, Vector2.Zero);
                //spriteBatch.Draw(InputRenderTexture, right, amplifiedColor, 0, Vector2.Zero);
                //spriteBatch.Draw(InputRenderTexture, textureOffset, amplifiedColor, 0, Vector2.Zero, Scale);
                spriteBatch.Draw(InputRenderTexture, Vector2.Zero, amplifiedColor, 0, Vector2.Zero);
                spriteBatch.End();
            }
        }

        protected override void Destroy()
        {
            base.Destroy();

            spriteBatch.Dispose();
        }

        private void InitializeOutlineRenderer()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            //textureOffset = new Vector2((InputRenderTexture.Width - InputRenderTexture.Width * Scale) / 2f,
            //    (InputRenderTexture.Height - InputRenderTexture.Height * Scale) / 2f);

            Services.AddService(this as IOutlineRenderer);
        }
    }
}
