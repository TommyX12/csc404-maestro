using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkingRenderer : MonoBehaviour
{
    public float scale = 30;
    public PlayerAgent player;
    new Renderer renderer;
    private float timer = 0;
    private void Start()
    {
        renderer = GetComponent<Renderer>();
    }
    private void Update()
    {
        timer += Time.fixedDeltaTime;
        if (player.invulnerabilityTimer >= 0)
        {
            float val = Mathf.Sin(timer * scale);
            renderer.enabled = val < 0 ? false : true;
        }
        else {
            renderer.enabled = true;
        }
    }
}
