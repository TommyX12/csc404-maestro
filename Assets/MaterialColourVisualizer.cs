using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialColourVisualizer : MonoBehaviour
{
    // HDR
    public bool useHDR = true;
    [Range(0, 1)]
    public float HDRDecay = 0;
    private float HDRMax = 1;

    public bool randomBand = false;
    public Material material;
    public Mesh mesh;
    public int[] band = new int[3];
    [Range(0,100)]
    public float scale = 1;
    private int colorID;
    private float[] average = new float[3];
    private MaterialPropertyBlock block;
    [Range(0, 1)]
    public float averageDecay = 0.5f;
    private void Start()
    {
        if (randomBand) {
            band[0]= Random.Range(0, 8);    
            band[1] = Random.Range(0, 8);
            band[2] = Random.Range(0, 8);
        }

        if (GetComponent<MeshRenderer>()) {
            GetComponent<MeshRenderer>().enabled = false;
        }

        if (!mesh) {
            mesh = GetComponent<MeshFilter>().mesh;
        }

        block = new MaterialPropertyBlock();
        colorID = Shader.PropertyToID("_Color");
    }

    private void OnGUI()
    {
        GUI.TextArea(new Rect(0, 0, 100, 100), HDRMax.ToString());
    }

    void Update()
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

        block.SetColor(colorID, new Color((average[0]/HDRMax) * scale, (average[1] / HDRMax) * scale, (average[2] / HDRMax) * scale));
        Graphics.DrawMesh(mesh, transform.position, transform.rotation, material, 0, null, 0, block);
    }
}
