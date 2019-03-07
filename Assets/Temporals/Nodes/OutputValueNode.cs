using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using XNode;

[CreateNodeMenu("Interface/Output Value")]
[NodeWidth(150)]
[NodeTint("#22ff22")]
public class OutputValueNode : TemporalNodeBase {

    [Input(connectionType = ConnectionType.Override)]
    public float value;
    
    public OutputValueNode() {
        
    }

    public object GetValue() {
        return GetInputValue<object>("value");
    }

    protected override void ComputeCache() {
        
    }
}
