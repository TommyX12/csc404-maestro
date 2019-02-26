using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisiBlock : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        MeshRenderer renderer;
        if (renderer = GetComponent<MeshRenderer>()) {
            renderer.enabled = false;
        }
    }
}
