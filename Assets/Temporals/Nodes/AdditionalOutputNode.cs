using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using XNode;

[CreateNodeMenu("Interface/Additional Output")]
[NodeTint("#22ff22")]
public class AdditionalOutputNode : TemporalNodeBase {

    public int index;
    public string name;

    [Input(connectionType = ConnectionType.Override)]
    public float value;
    
    public AdditionalOutputNode() {
        
    }

    public object GetValue() {
        return GetInputValue<object>("value");
    }

    protected override void ComputeCache() {
        
    }
    
}
