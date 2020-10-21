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
        public PostProcessingEffects PostProcessingEffects { get; set; }
        public float Scale { get; set; } = 1.01f;

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

        private const float ColorAmplifier = 10f;

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
            
            spriteBatch.Draw(OutlineRenderTarget, textureOffset, amplifiedColor, 0, Vector2.Zero,Scale);
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
