using Stride.Core;
using Stride.Core.Mathematics;
using Stride.Games;
using Stride.Graphics;
using Stride.Rendering;
using Stride.Rendering.Compositing;

namespace StrideOutlineRenderer.Renderers
{
    [Display("Outline Renderer")]
    public class OutlineRenderer : SceneRendererBase
    {
        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        // ReSharper disable once MemberCanBePrivate.Global
        public Texture OutlineRenderTarget { get; set; }
        public float Scale { get; set; } = 1.1f;

        private SpriteBatch spriteBatch;
        private Vector2 textureOffset;

        protected override void InitializeCore()
        {
            base.InitializeCore();

            spriteBatch = new SpriteBatch(GraphicsDevice);

            textureOffset = new Vector2((OutlineRenderTarget.Width - OutlineRenderTarget.Width * Scale) / 2,
                (OutlineRenderTarget.Height - OutlineRenderTarget.Height * Scale) / 2);
        }

        protected override void DrawCore(RenderContext context, RenderDrawContext drawContext)
        {
            spriteBatch.Begin(drawContext.GraphicsContext, depthStencilState: DepthStencilStates.None);

            // TODO adjust for offset
            spriteBatch.Draw(OutlineRenderTarget, textureOffset, Color.Red, 0, Vector2.Zero,Scale);
            //spriteBatch.Draw(OutlineRenderTarget, Vector2.Zero);
            spriteBatch.End();
        }

        protected override void Destroy()
        {
            base.Destroy();

            spriteBatch.Dispose();
        }
    }
}