using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMaterial : MonoBehaviour
{
    public Material material;
    public void Change() {
        GetComponent<Renderer>().material = material;
        GetComponent<Renderer>().SetPropertyBlock(null);
    }
}
