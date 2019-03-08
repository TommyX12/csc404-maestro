using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using XNode;

[CreateNodeMenu("Modifiers/Repeater")]
public class RepeaterNode : BasicTemporalNode {

    [Input(connectionType = ConnectionType.Override)]
    public float cycle = 4; // beats
    
    public RepeaterNode() {
        
    }

    protected override void ComputeCache() {
        float time = GetInputValue<float>("time", this.time);
        float cycle = GetInputValue<float>("cycle", this.cycle);
        float timeCycle = GetTime(cycle);
        SetCachedOutput("afterTime", time % timeCycle);
    }
}
