using Stride.Core;
using Stride.Core.Diagnostics;
using Stride.Core.Mathematics;
using Stride.Engine;
using Stride.Rendering;
using StrideOutlineRenderer.Renderers;
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace StrideOutlineRenderer.Component
{
    [DataContract("OutlineComponent")]
    [Display("Outline", Expand = ExpandRule.Once)]
    [ComponentOrder(1101)]
    [ComponentCategory("Model")]
    public class OutlineComponent : StartupScript
    {
        public bool AlwaysOutlined { get; set; } = false;
        public Color HighlightColor { get; set; }
        public RenderGroup OutlineRenderGroup { get; set; } = RenderGroup.Group10; // highlighted render group

        private ModelComponent modelComponent;
        private PhysicsComponent physicsComponent;
        private IOutlineRenderer outlineRenderer;
        private RenderGroup configuredRenderGroup; // un-highlighted render group

        public override void Start()
        {
            InitializeOutlineComponent();
        }

        public void Enable()
        {
            if (outlineRenderer == null)
            {
                outlineRenderer = Services.GetService<IOutlineRenderer>();
            }

            modelComponent.RenderGroup = OutlineRenderGroup;
            outlineRenderer?.ChangeColor(HighlightColor);
        }

        public void Disable()
        {
            modelComponent.RenderGroup = configuredRenderGroup;
        }

        private void InitializeOutlineComponent()
        {
            Log.ActivateLog(LogMessageType.Info);

            modelComponent = Entity.Get<ModelComponent>();
            if (modelComponent == null) 
            {
                Log.Error($"Model component not found on outline component.");
                //throw new NullReferenceException($"Model component not found on outline component.");
            }
            else
            {
                configuredRenderGroup = modelComponent.RenderGroup;
            }

            // Model outline highlighting uses physics ray casting in BasicCameraController
            physicsComponent = Entity.Get<PhysicsComponent>();
            if (physicsComponent == null && AlwaysOutlined != true) 
            {
                Log.Error($"Physics collider component not found on outline component.");
                //throw new NullReferenceException($"Collider component not found outline component.");
            }

            if (AlwaysOutlined)
            {
                Enable();
            }
        }
    }
}
