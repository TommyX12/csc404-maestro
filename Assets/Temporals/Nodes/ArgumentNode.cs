using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using XNode;

[CreateNodeMenu("Argument")]
[NodeTint("#22ff22")]
public class ArgumentNode : TemporalNodeBase {

    protected float _value;

    public int index;
    public string name;

    [Output]
    public float value;
    
    public ArgumentNode() {
        
    }

    public void SetValue(float value) {
        _value = value;
    }

    protected override void ComputeCache() {
        cachedOutputs[GetOutputPort("value")] = _value;
    }
    
}
