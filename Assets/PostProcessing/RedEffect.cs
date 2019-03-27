using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

[Serializable]
[PostProcess(typeof(RedEffectRenderer), PostProcessEvent.BeforeStack, "Custom/RedEffect", true)]
public sealed class RedEffect : PostProcessEffectSettings
{
    [Range(0f, 1f), Tooltip("blending")]
    public FloatParameter blend = new FloatParameter { value = 0.5f };
}

public sealed class RedEffectRenderer : PostProcessEffectRenderer<RedEffect>
{
    public override void Render(PostProcessRenderContext context)
    {
        var sheet = context.propertySheets.Get(Shader.Find("Hidden/Custom/RedEffect"));
        sheet.properties.SetFloat("_Blend", settings.blend);
        context.command.BlitFullscreenTriangle(context.source, context.destination, sheet, 0);
    }
}