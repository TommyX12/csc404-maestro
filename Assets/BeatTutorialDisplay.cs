using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatTutorialDisplay : MonoBehaviour
{

    public float fadeTime = 1;
    private float pulseTimer = 0;

    public Color pulseColor;
    public Color fadeColor;

    public Sprite hitSprite;
    public Sprite missSprite;

    public SpriteRenderer spriteRenderer;

    int colorID;
    MaterialPropertyBlock block;
    private new Renderer renderer;
    private void Start()
    {
        renderer = GetComponent<Renderer>();
        colorID = Shader.PropertyToID("_Color");
        block = new MaterialPropertyBlock();
    }


    public void Pulse() { pulseTimer = fadeTime; }

    public void SetState(int state) {
        spriteRenderer.sprite = state == 0 ? missSprite : state == 1 ? hitSprite : null;
    }

    private void Update()
    {
        pulseTimer -= Time.deltaTime;
        Color col = Color.Lerp(fadeColor, pulseColor, Mathf.Max(pulseTimer / fadeTime, 0));
        block.SetColor(colorID, col);
        renderer.SetPropertyBlock(block);
    }
}
