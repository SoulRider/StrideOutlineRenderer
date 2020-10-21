using Stride.Core;
using Stride.Core.Mathematics;
using Stride.Engine;
using Stride.Graphics;
// ReSharper disable MemberCanBePrivate.Global

namespace StrideOutlineRenderer.Renderers
{
    public class OutlineRenderTargetClear : SyncScript
    {
        private readonly Color4 transparent = new Color4(0, 0, 0, 0);

        [Display(category: "Rendering")] public Texture OutlineRenderTarget;
        public override void Update()
        {
            Game.GraphicsContext.CommandList.Clear(OutlineRenderTarget, transparent);
        }
    }
}
