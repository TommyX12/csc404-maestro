using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlatColor : TemporalController
{
    public Color baseColor = Color.gray;
    private int colorID;
    private MaterialPropertyBlock block;

    private void Start()
    {
        block = new MaterialPropertyBlock();
        colorID = Shader.PropertyToID("_Color");
    }

    public Dictionary<DeterministicObject, Renderer> renderers = new Dictionary<DeterministicObject, Renderer>();

    public override void Determine(DeterministicObject obj, float time)
    {
        if (block == null)
        {
            Start();
        }

        block.SetColor(colorID, baseColor);

        // don't really need time for this one
        if (!renderers.ContainsKey(obj))
        {
            Initialize(obj);
        }

        Renderer rendy = renderers[obj];
        block.SetColor(colorID, baseColor);
        rendy.SetPropertyBlock(block);
    }

    public override void Initialize(DeterministicObject obj)
    {
        renderers[obj] = ((UniversalCube)obj).rendy;
    }
}
