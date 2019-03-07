using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using XNode;

[CreateNodeMenu("Modifiers/Delay")]
public class DelayNode : BasicTemporalNode {

    [Input(connectionType = ConnectionType.Override)]
    public float amount = 4; // beats
    
    public DelayNode() {
        
    }

    protected override void ComputeCache() {
        float time = GetInputValue<float>("time", this.time);
        float amount = GetInputValue<float>("amount", this.amount);
        float timeAmount = GetTime(amount);
        SetCachedOutput("afterTime", time - timeAmount);
    }
}
