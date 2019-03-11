using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SineColorPulse : MonoBehaviour
{

    public Color colorOne;
    public Color colorTwo;

    public float scale = 3;
    public new Renderer renderer;
    private int colorID;
    private MaterialPropertyBlock block;

    private void Start()
    {
        colorID = Shader.PropertyToID("_Color");
        block = new MaterialPropertyBlock();
    }

    private void Update()
    {
        float interpVal = Mathf.Sin(Time.fixedTime * scale);
        Color color = Color.Lerp(colorOne, colorTwo, (interpVal + 1f) / 2f);
        block.SetColor(colorID, color);
        renderer.SetPropertyBlock(block);
    }
}
