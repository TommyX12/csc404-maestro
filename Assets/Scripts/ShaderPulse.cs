using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShaderPulse : MonoBehaviour
{
    public List<Material> materials;
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
        foreach (Material m in materials)
        {
            m.SetFloat(pulseID, pulse);
            if (pulse > 0)
            {
                pulse -= Time.deltaTime;
            }
        }
    }


}
