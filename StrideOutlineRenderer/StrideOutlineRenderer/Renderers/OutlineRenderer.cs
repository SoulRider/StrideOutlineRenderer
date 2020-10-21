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
        public float Scale { get; set; } = 1.01f;

        public Color Color
        {
            get => _color;
            set
            {
                _color = value;
                _amplifiedColor = new Color4(
                    Color.R * ColorAmplifier, 
                    Color.G * ColorAmplifier,
                    Color.B * ColorAmplifier, 
                    ColorAmplifier);
            }
        }

        private SpriteBatch _spriteBatch;
        private Vector2 _textureOffset;

        private Color _color;
        private Color4 _amplifiedColor;

        private const float ColorAmplifier = 10f;

        protected override void InitializeCore()
        {
            base.InitializeCore();

            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _textureOffset = new Vector2((OutlineRenderTarget.Width - OutlineRenderTarget.Width * Scale) / 2,
                (OutlineRenderTarget.Height - OutlineRenderTarget.Height * Scale) / 2);
        }

        protected override void DrawCore(RenderContext context, RenderDrawContext drawContext)
        {
            _spriteBatch.Begin(drawContext.GraphicsContext, depthStencilState: DepthStencilStates.None);

            _spriteBatch.Draw(OutlineRenderTarget, _textureOffset, _amplifiedColor, 0, Vector2.Zero,Scale);
            _spriteBatch.Draw(OutlineRenderTarget, Vector2.Zero);
            _spriteBatch.End();
        }

        protected override void Destroy()
        {
            base.Destroy();

            _spriteBatch.Dispose();
        }
    }
}
