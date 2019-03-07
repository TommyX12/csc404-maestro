using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using XNode;

[CreateNodeMenu("Primitive Waves/Sin Wave")]
public class SinWaveNode : BasicTemporalNode {

    [Input(connectionType = ConnectionType.Override)]
    public float minValue = 0;

    [Input(connectionType = ConnectionType.Override)]
    public float maxValue = 1;
    
    [Input(connectionType = ConnectionType.Override)]
    public float cycle = 1; // n means 1 cycle is every n beats
    
    [Input(connectionType = ConnectionType.Override)]
    public float phase = 0; // 1 means shift graph to the right by 1 cycle
    
    [Input(connectionType = ConnectionType.Override)]
    public float sharpness = 1; // power applied to the wave
    
    public SinWaveNode() {
        
    }

    protected override void Init() {
        
    }

    protected override void ComputeCache() {
        float time = GetInputValue<float>("time", this.time);
        float minValue = GetInputValue<float>("minValue", this.minValue);
        float maxValue = GetInputValue<float>("maxValue", this.maxValue);
        float cycle = GetInputValue<float>("cycle", this.cycle);
        float phase = GetInputValue<float>("phase", this.phase);
        float sharpness = GetInputValue<float>("sharpness", this.sharpness);
        
        float beats = GetBeats(time);
        float value = Mathf.Cos(((beats * (2 * Mathf.PI)) - phase) / cycle) * 0.5f + 0.5f;
        int sign = value > 0 ? 1 : -1;
        value = sign * Mathf.Pow(Mathf.Abs(value), sharpness);
        value = value * (maxValue - minValue) + minValue;

        cachedOutputs[GetOutputPort("afterTime")] = time;
        cachedOutputs[GetOutputPort("value")] = value;
    }
    
}
