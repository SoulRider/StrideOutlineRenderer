using Stride.Core;
using Stride.Core.Mathematics;
using Stride.Games;
using Stride.Graphics;
using Stride.Rendering;
using Stride.Rendering.Compositing;
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable once UnusedAutoPropertyAccessor.Global

namespace StrideOutlineRenderer.Renderers
{
    [Display("Outline Renderer")]
    public class OutlineRenderer : SceneRendererBase
    {


        public Texture OutlineRenderTarget { get; set; }
        public float Scale { get; set; } = 1.1f;
        public Color Color { get; set; } = Color.Red;

        private SpriteBatch spriteBatch;
        private Vector2 textureOffset;

        private static readonly Color ColorMultiplier = new Color(10f, 10f, 10f);

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
            spriteBatch.Draw(OutlineRenderTarget, textureOffset, Color, 0, Vector2.Zero,Scale);
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