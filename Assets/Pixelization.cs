using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pixelization : MonoBehaviour
{
    public Material material;
    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Debug.Log("called");
        Graphics.Blit(source, destination, material);
    }
}
