using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using XNode;

[CreateNodeMenu("Math/Mul")]
public class MulNode : TemporalNodeBase {

    [Input(connectionType = ConnectionType.Override)]
    public float a = 1;
    
    [Input(connectionType = ConnectionType.Override)]
    public float b = 1;

    [Output]
    public float value = 2;
    
    public MulNode() {
        
    }

    protected override void ComputeCache() {
        float a = GetInputValue<float>("a", this.a);
        float b = GetInputValue<float>("b", this.b);
        SetCachedOutput("value", a * b);
    }
}
