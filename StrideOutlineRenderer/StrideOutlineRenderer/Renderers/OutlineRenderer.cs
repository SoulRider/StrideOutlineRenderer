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

        private SpriteBatch spriteBatch;

        protected override void InitializeCore()
        {
            base.InitializeCore();

            spriteBatch = new SpriteBatch(GraphicsDevice);

            Services.GetService<IGame>().IsMouseVisible = false;
        }

        protected override void DrawCore(RenderContext context, RenderDrawContext drawContext)
        {
            spriteBatch.Begin(drawContext.GraphicsContext, depthStencilState: DepthStencilStates.None);

            // TODO adjust for offset
            spriteBatch.Draw(OutlineRenderTarget, Vector2.Zero, Color.Red, 0, Vector2.Zero,2);
            spriteBatch.Draw(OutlineRenderTarget, Vector2.Zero);
            spriteBatch.End();
        }

        protected override void Destroy()
        {
            base.Destroy();

            spriteBatch.Dispose();
        }
    }
}