using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

[Serializable]
[PostProcess(typeof(GrayEffectRenderer), PostProcessEvent.AfterStack, "Custom/GrayEffect", false)]
public sealed class GrayEffect : PostProcessEffectSettings
{
    [Range(0f, 1f), Tooltip("Grayscale effect intensity.")]
    public FloatParameter blend = new FloatParameter { value = 0.5f };

    [Range(0, 100), Tooltip("dum cereal")]
    public IntParameter dummy = new IntParameter { value = 1 };
}

public sealed class GrayEffectRenderer : PostProcessEffectRenderer<GrayEffect>
{
    public override void Render(PostProcessRenderContext context)
    {
        var sheet = context.propertySheets.Get(Shader.Find("Hidden/Custom/GrayscaleEffect"));
        sheet.properties.SetFloat("_Blend", settings.blend);
        sheet.properties.SetInt("_Dummy", settings.dummy);
        context.command.BlitFullscreenTriangle(context.source, context.destination, sheet, 0);
    }
}