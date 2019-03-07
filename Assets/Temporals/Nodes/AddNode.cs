using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using XNode;

[CreateNodeMenu("Math/Add")]
public class AddNode : TemporalNodeBase {

    [Input(connectionType = ConnectionType.Override)]
    public float a = 1;
    [Input(connectionType = ConnectionType.Override)]
    public float b = 1;

    [Output]
    public float value = 2;
    
    public AddNode() {
        
    }

    protected override void ComputeCache() {
        float a = GetInputValue<float>("a", this.a);
        float b = GetInputValue<float>("b", this.b);
        SetCachedOutput("value", a + b);
    }
}
