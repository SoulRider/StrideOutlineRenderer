using System;
using Stride.Core;
using Stride.Core.Mathematics;
using Stride.Engine;
using Stride.Rendering;
using StrideOutlineRenderer.Renderers;
// ReSharper disable MemberCanBePrivate.Global

namespace StrideOutlineRenderer.Component
{
    [DataContract("OutlineComponent")]
    [Display("Outline", Expand = ExpandRule.Once)]
    [ComponentOrder(1101)]
    [ComponentCategory("Model")]
    public class OutlineComponent : StartupScript
    {
        public Color HighlightColor;
        public bool AlwaysOutlined = false;

        private ModelComponent modelComponent;
        private PhysicsComponent physicsComponent;
        private IOutlineRenderer outlineRenderer;

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

            modelComponent.RenderGroup = RenderGroup.Group10;
            outlineRenderer?.ChangeColor(HighlightColor);
        }

        public void Disable()
        {
            modelComponent.RenderGroup = RenderGroup.Group0;
        }

        private void InitializeOutlineComponent()
        {
            modelComponent = Entity.Get<ModelComponent>();
            if (modelComponent == null) 
            {
                throw new NullReferenceException($"Model component not found on outline component.");
            }

            physicsComponent = Entity.Get<PhysicsComponent>();
            if (physicsComponent == null) 
            {
                throw new NullReferenceException($"Collider component not found outline component.");
            }

            if (AlwaysOutlined)
            {
                Enable();
            }
        }
    }
}
