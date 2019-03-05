using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class FrequencyToColor : TemporalController
{
    public Dictionary<DeterministicObject, Renderer> renderers = new Dictionary<DeterministicObject, Renderer>();
    public Dictionary<DeterministicObject, Mesh> meshes = new Dictionary<DeterministicObject, Mesh>();

    public Material material;
    public Color baseColor = Color.gray;

    // HDR
    public bool useHDR = true;
    [Range(0, 1)]
    public float HDRDecay = 0;
    private float HDRMax = 1;
    public bool randomBand = false;
    public int[] band = new int[3];
    [Range(0, 100)]
    public float scale = 1;
    private int colorID;
    private float[] average = new float[3];
    private MaterialPropertyBlock block;
    [Range(0, 1)]
    public float averageDecay = 0.5f;

    private void Start()
    {
        if (randomBand)
        {
            band[0] = Random.Range(0, 8);
            band[1] = Random.Range(0, 8);
            band[2] = Random.Range(0, 8);
        }

        block = new MaterialPropertyBlock();
        colorID = Shader.PropertyToID("_Color");
    }

    public override void Initialize(DeterministicObject obj)
    {
        renderers[obj] = obj.GetComponentInChildren<MeshRenderer>();
        meshes[obj] = obj.GetComponentInChildren<MeshFilter>().sharedMesh;
    }

    private void Update()
    {
        average[0] = average[0] * (1 - averageDecay) + (averageDecay) * FrequencyBander.GetBand(band[0]);
        average[1] = average[1] * (1 - averageDecay) + (averageDecay) * FrequencyBander.GetBand(band[1]);
        average[2] = average[2] * (1 - averageDecay) + (averageDecay) * FrequencyBander.GetBand(band[2]);
        if (useHDR)
        {
            HDRMax = Mathf.Max(HDRMax, FrequencyBander.GetBand(band[0]),
                FrequencyBander.GetBand(band[1]),
                FrequencyBander.GetBand(band[2]));
            HDRMax = HDRMax * (1 - HDRDecay * Time.deltaTime);
        }
    }

    public override void Determine(DeterministicObject obj, float time)
    {
        if (block==null) {
            Start();
        }

        Color audioColor = new Color((average[0] / HDRMax) * scale, (average[1] / HDRMax) * scale, (average[2] / HDRMax) * scale);
        block.SetColor(colorID, baseColor + audioColor);

        // don't really need time for this one
        if (!renderers.ContainsKey(obj)) {
            Initialize(obj);
        }
        Mesh mesh = meshes[obj];
        Renderer rendy = renderers[obj];

        block.SetColor(colorID, baseColor + audioColor);
        rendy.SetPropertyBlock(block);
    }
}
