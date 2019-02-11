using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShaderPulse : MonoBehaviour
{
    public Material material;
    private int pulseID;
    float pulse = 0;

    private void Start()
    {
        pulseID = Shader.PropertyToID("_pulse");
    }   

    public void Pulse() {
        pulse = 1;
    }

    public void Update()
    {
        material.SetFloat(pulseID, pulse);
        if (pulse > 0) {
            pulse -= Time.deltaTime;
        }
    }


}
