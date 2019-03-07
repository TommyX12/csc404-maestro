using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using XNode;

[CreateNodeMenu("Interface/Input Time")]
[NodeWidth(150)]
[NodeTint("#22ff22")]
public class InputTimeNode : TemporalNodeBase {

    protected float _time;

    [Output]
    public float time;
    
    public InputTimeNode() {
        
    }

    public void SetTime(float time) {
        _time = time;
    }

    public object GetValue() {
        return GetValue("time");
    }

    protected override void ComputeCache() {
        cachedOutputs[GetOutputPort("time")] = _time;
    }

}
