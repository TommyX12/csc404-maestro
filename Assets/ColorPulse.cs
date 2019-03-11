using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorPulse : TemporalController
{

    public TemporalController fallback;
    public Dictionary<DeterministicObject, Renderer> renderers = new Dictionary<DeterministicObject, Renderer>();
    public AnimationCurve pulseCurve = AnimationCurve.Linear(0, 0, 1, 1);

    public float interpTime = 1;
    public Color startColor = Color.white;
    public Color endColor = Color.black;

    private MaterialPropertyBlock block;
    private int colorID;

    private bool setup = false;

    private void Setup() {
        colorID = Shader.PropertyToID("_Color");
        block = new MaterialPropertyBlock();
        setup = true;
    }

    private void Start()
    {
        if (!setup) {
            Setup();
        }
    }

    public override void Determine(DeterministicObject obj, float time)
    {
        if (!setup || block == null) {
            Setup();
        }

        // use fallback
        if (fallback != null && time >= interpTime) {
            fallback.Determine(obj, time);
            return;
        }

        Color col = Color.Lerp(startColor, endColor, pulseCurve.Evaluate(Mathf.Min(1, time / interpTime)));
        block.SetColor(colorID, col);
        renderers[obj].SetPropertyBlock(block);
    }

    public override void Initialize(DeterministicObject obj)
    {
        UniversalCube cube = (UniversalCube)obj;
        renderers[obj] = cube.rendy;

        if (fallback != null)
        {
            Determine(obj, interpTime);
            fallback.Initialize(obj);
            Determine(obj, 0);
        }
    }
}
