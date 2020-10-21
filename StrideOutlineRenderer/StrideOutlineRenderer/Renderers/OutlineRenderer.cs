using Stride.Core;
using Stride.Core.Mathematics;
using Stride.Engine;
using Stride.Games;
using Stride.Graphics;
using Stride.Rendering;
using Stride.Rendering.Compositing;
using Stride.Rendering.Images;

// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable once UnusedAutoPropertyAccessor.Global

namespace StrideOutlineRenderer.Renderers
{
    [Display("Outline Renderer")]
    public class OutlineRenderer : SceneRendererBase
    {
        public Texture OutlineRenderTarget { get; set; }
        public float Scale { get; set; } = 1.005f;

        public Color Color
        {
            get => color;
            set
            {
                color = value;
                amplifiedColor = new Color4(
                    Color.R * ColorAmplifier, 
                    Color.G * ColorAmplifier,
                    Color.B * ColorAmplifier, 
                    ColorAmplifier);
            }
        }

        private SpriteBatch spriteBatch;
        private Vector2 textureOffset;

        private Color color;
        private Color4 amplifiedColor;

        private readonly Vector2 up = -Vector2.UnitY * VerticalOffset;
        private readonly Vector2 down = Vector2.UnitY * VerticalOffset;
        private readonly Vector2 left = -Vector2.UnitX * HorizontalOffset;
        private readonly Vector2 right = Vector2.UnitX * HorizontalOffset;

        private const float ColorAmplifier = 10f;
        private const float VerticalOffset = 2f;
        private const float HorizontalOffset = 3f;

        protected override void InitializeCore()
        {
            base.InitializeCore();

            spriteBatch = new SpriteBatch(GraphicsDevice);

            textureOffset = new Vector2((OutlineRenderTarget.Width - OutlineRenderTarget.Width * Scale) / 2f,
                (OutlineRenderTarget.Height - OutlineRenderTarget.Height * Scale) / 2f);
        }

        protected override void DrawCore(RenderContext context, RenderDrawContext drawContext)
        {
            spriteBatch.Begin(drawContext.GraphicsContext, depthStencilState: DepthStencilStates.None);
            
            spriteBatch.Draw(OutlineRenderTarget, up, amplifiedColor, 0, Vector2.Zero, 1);
            spriteBatch.Draw(OutlineRenderTarget, down, amplifiedColor, 0, Vector2.Zero, 1);

            spriteBatch.Draw(OutlineRenderTarget, left, amplifiedColor, 0, Vector2.Zero, 1);
            spriteBatch.Draw(OutlineRenderTarget, right, amplifiedColor, 0, Vector2.Zero, 1);

            spriteBatch.Draw(OutlineRenderTarget, textureOffset, amplifiedColor, 0, Vector2.Zero, Scale);

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
