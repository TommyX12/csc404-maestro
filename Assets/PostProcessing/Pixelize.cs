using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

[Serializable]
[PostProcess(typeof(PixelizeRenderer), PostProcessEvent.BeforeStack, "Custom/Pixelize", true)]
public sealed class Pixelize: PostProcessEffectSettings
{
    [Range(0f, 1f), Tooltip("blending")]
    public FloatParameter blend = new FloatParameter { value = 0.5f };

    [Range(1,100), Tooltip("PixelSize")]
    public IntParameter pixelSize = new IntParameter { value = 1 };
}

public sealed class PixelizeRenderer : PostProcessEffectRenderer<Pixelize>
{
    public override void Render(PostProcessRenderContext context)
    {
        var sheet = context.propertySheets.Get(Shader.Find("Hidden/Custom/Pixelize"));
        sheet.properties.SetFloat("_Blend", settings.blend);
        sheet.properties.SetInt("_PixelSize", settings.pixelSize);
        context.command.BlitFullscreenTriangle(context.source, context.destination, sheet, 0);
    }
}