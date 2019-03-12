using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class SimpleMaterialPulse : MonoBehaviour
{
    public float frequency = 1;
    public float offset = 0;

    public Color color1 = Color.black;
    public Color color2 = Color.white;

    private MaterialPropertyBlock block;
    private int colorID;

    private Renderer rendy;

    private void Awake()
    {
        block = new MaterialPropertyBlock();
        colorID = Shader.PropertyToID("_Color");
        rendy = GetComponent<Renderer>();
    }

    private void Update()
    {
        Color col = Color.Lerp(color1, color2, (Mathf.Sin((Time.fixedTime + offset)*frequency) + 1f)/2f);
        block.SetColor(colorID, col);
        rendy.SetPropertyBlock(block);
    }

    private void OnDisable()
    {
        rendy.SetPropertyBlock(null);
    }

}
