using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using XNode;

[CreateNodeMenu("Interface/Output Time")]
[NodeWidth(150)]
[NodeTint("#22ff22")]
public class OutputTimeNode : TemporalNodeBase {

    [Input(connectionType = ConnectionType.Override)]
    public float afterTime;
    
    public OutputTimeNode() {
        
    }

    public object GetValue() {
        return GetInputValue<object>("afterTime");
    }

    protected override void ComputeCache() {
        
    }
}
