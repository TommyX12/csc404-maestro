using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using XNode;

[CreateNodeMenu("Range/Interval")]
public class IntervalNode : BasicTemporalNode {

    [Input(connectionType = ConnectionType.Override)]
    public float start = 0; // beats
    
    [Input(connectionType = ConnectionType.Override)]
    public float end = 1; // beats

    [Input(connectionType = ConnectionType.Override)]
    public float softMargin = 0.5f; // beats
    
    public IntervalNode() {
        
    }

    protected override void ComputeCache() {
        float time = GetInputValue<float>("time", this.time);
        float start = GetInputValue<float>("start", this.start);
        float end = GetInputValue<float>("end", this.end);
        float softMargin = GetInputValue<float>("softMargin", this.softMargin);
        float beats = GetBeats(time);
        float value = 0;
        if (beats >= start && beats <= end) {
            value = 1;
        }
        else if (beats < start && beats >= start - softMargin) {
            value = (beats - (start - softMargin)) / (softMargin);
        }
        else if (beats > end && beats <= end + softMargin) {
            value = ((end + softMargin) - beats) / (softMargin);
        }
        SetCachedOutput("value", value);
        SetCachedOutput("afterTime", GetTime(beats - end));
    }
}
