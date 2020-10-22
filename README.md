# StrideOutlineRenderer
Stride Outline Renderer - Renderer for Rendering 3D Object Outlines

 <img src="Screenshots/StrideOutlineRenderer.png">

##Features:
- Halo/Bloom/Blend outine effect for model components (this is not a shader)
- Enable outline on model components by assigning specified render group at runtime
- Disable outline on model components by assigning specified render group at runtime
- Permanently enable outline on model components by assigning specified render group at runtime and "AlwaysOutlined"
- Performance should scale (horizontally) directly with PostFX Bloom process

## How it works:
- Model components are set to RenderGroup9, when they are highlighted they are changed to RenderGroup10
- Model components have an OutlineComponent attached to them, assigning a highlight color and outline RenderGroup
- RenderTextureSceneRenderer in Compositor writes only RenderGroup10 to a render texture (InputRenderTexture)
- OutlineRenderer uses SpriteBatch to write a new RenderTexture (OutputRenderTexture) by saturating color
- OutlineUiOutput Entity/UiComponent in Scene renders the OutputRenderTexture to the camera as RenderGroup30
- RenderGroup30 has PostFX applied through SharedRenderer (This is how the bloom occurs)
- InputRenderTexture is excluded from PostFX, but added as a SingleStageRenderer for Transparent pass "on top of" bloom effect

Scene:
<img src="Screenshots/Scene.png">

Compositor:
 <img src="Screenshots/Compositor.png">
 
Compositor Expanded:
<img src="Screenshots/CompositorExpanded.png">
  
Compositor Render Texture (Item 0):
<img src="Screenshots/GroupRenderToTexture.png">

Compositor Outline Renderer (Item 1):
<img src="Screenshots/OutlineRenderer.png">

Compositor Shared Renderer to Post FX Omit Group 9 (Item 2):
<img src="Screenshots/OmiGroupSharedRenderer.png">

Compositor Group 9 Single Stag Renderere:
<img src="Screenshots/OmitGroupSharedRenderer.png">

Compositor Group 9 Single Stag Renderere:
<img src="Screenshots/OmitGroupSharedRenderer.png">

Compositor Group 31 Single Stage Renderer:
<img src="Screenshots/Group31SingleStage.png">
 
## Potential Improvements
- You could combine the RenderTextureSceneRenderer functionality into the OutlineRenderer

## Other Implementations
- Check my profile, I will also release the non-bloom version with a conmbined Renderer (RenderTexture and SpriteBatcher).


