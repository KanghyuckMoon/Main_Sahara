using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;
public abstract class SkyRenderer
{
    int m_LastFrameUpdate = -1;

    /// <summary>
    /// Called on startup. Create resources used by the renderer (shaders, materials, etc).
    /// </summary>
    public abstract void Build();

    /// <summary>
    /// Called on cleanup. Release resources used by the renderer.
    /// </summary>
    public abstract void Cleanup();

    /// <summary>
    /// HDRP calls this function once every frame. Implement it if your SkyRenderer needs to iterate independently of the user defined update frequency (see SkySettings UpdateMode).
    /// </summary>
    /// <returns>True if the update determines that sky lighting needs to be re-rendered. False otherwise.</returns>
    protected virtual bool Update(BuiltinSkyParameters builtinParams) { return false; }

    /// <summary>
    /// Implements actual rendering of the sky. HDRP calls this when rendering the sky into a cubemap (for lighting) and also during main frame rendering.
    /// </summary>
    /// <param name="builtinParams">Engine parameters that you can use to render the sky.</param>
    /// <param name="renderForCubemap">Pass in true if you want to render the sky into a cubemap for lighting. This is useful when the sky renderer needs a different implementation in this case.</param>
    /// <param name="renderSunDisk">If the sky renderer supports the rendering of a sun disk, it must not render it if this is set to false.</param>
    public abstract void RenderSky(BuiltinSkyParameters builtinParams, bool renderForCubemap, bool renderSunDisk);
}