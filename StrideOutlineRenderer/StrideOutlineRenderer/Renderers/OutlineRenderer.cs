using Stride.Core;
using Stride.Core.Mathematics;
using Stride.Graphics;
using Stride.Rendering;
using Stride.Rendering.Compositing;
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable once UnusedAutoPropertyAccessor.Global

namespace StrideOutlineRenderer.Renderers
{
    [Display("Outline Renderer")]
    public class OutlineRenderer : SceneRendererBase, IOutlineRenderer
    {
        public Texture InputRenderTexture { get; set; }
        public Texture OutputRenderTexture { get; set; }
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

        private SpriteBatch spriteBatch;

        private Color highlightColor;
        private Color4 amplifiedColor;

        private const float ColorAmplifier = 10f; // completely saturate the sprite

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
            /* This class is basically a render texture color tinting utility, it's used to prep for post-fx bloom */

            if (InputRenderTexture == null || OutputRenderTexture == null)
                return;

            using (drawContext.PushRenderTargetsAndRestore())
            {
                var depthBuffer = PushScopedResource(context.Allocator.GetTemporaryTexture2D(OutputRenderTexture.ViewWidth, OutputRenderTexture.ViewHeight, drawContext.CommandList.DepthStencilBuffer.ViewFormat, TextureFlags.DepthStencil));
                drawContext.CommandList.SetRenderTargetAndViewport(depthBuffer, OutputRenderTexture);

                spriteBatch.Begin(drawContext.GraphicsContext, depthStencilState: DepthStencilStates.None);
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
            Services.AddService(this as IOutlineRenderer);
        }
    }
}
