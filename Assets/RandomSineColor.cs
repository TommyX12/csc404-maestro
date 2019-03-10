using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSineColor : TemporalController
{

    public Color colorOne;
    public Color colorTwo;
    public float range = 3;

    private Dictionary<DeterministicObject, float> sineScales = new Dictionary<DeterministicObject, float>();
    private Dictionary<DeterministicObject, float> sineOffsets = new Dictionary<DeterministicObject, float>();
    private Dictionary<DeterministicObject, Renderer> renderers = new Dictionary<DeterministicObject, Renderer>();

    private int colorID;
    private MaterialPropertyBlock block;
    bool started = false;

    private void Init() {
        if (!started) {
            colorID = Shader.PropertyToID("_Color");
            block = new MaterialPropertyBlock();
            started = true;
        }
    }

    private void Start()
    {
        Init();
    }

    public override void Determine(DeterministicObject obj, float time)
    {
        if (!started) {
            Init();
        }
        float interpVal = Mathf.Sin((sineOffsets[obj] + time) * sineScales[obj]);
        Color color = Color.Lerp(colorOne, colorTwo, (interpVal + 1f) / 2f);
        block.SetColor(colorID, color);
        renderers[obj].SetPropertyBlock(block);
    }

    public override void Initialize(DeterministicObject obj)
    {
        sineScales[obj] = Random.Range(3f, -3f);
        sineOffsets[obj] = Random.Range(-2f * Mathf.PI, 2f * Mathf.PI);
        UniversalCube cube = (UniversalCube)obj;
        renderers[obj] = cube.rendy;
    }
}
