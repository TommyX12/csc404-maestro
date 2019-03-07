using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using XNode;

[CreateNodeMenu("General/Sub-graph")]
public class SubgraphNode : SubgraphNodeBase {
    
    [Input(connectionType = ConnectionType.Override)]
    public float time;

    [Output]
    public float value;

    [Output]
    public float afterTime;

    [Input(instancePortList = true, connectionType = ConnectionType.Override)]
    public float[] arguments;

    [Output(instancePortList = true)]
    public float[] additionalOutputs;
    
    public SubgraphNode() {
        
    }

    protected override void ComputeCache() {
        if (!subGraph) {
            Debug.LogError("subGraph not set.");
            return;
        }
        
        subGraph.InvalidateCache();
        int i;

        // Set inputs.
        subGraph.SetInputTime(GetInputValue<float>("time"));
        i = 0;
        foreach (NodePort port in InstanceInputs) {
            subGraph.SetArgument(i, port.GetInputValue<float>());
            ++i;
        }

        // Get outputs.
        cachedOutputs[GetOutputPort("value")] = subGraph.GetOutputValue();
        cachedOutputs[GetOutputPort("afterTime")] = subGraph.GetOutputTime();
        i = 0;
        foreach (NodePort port in InstanceOutputs) {
            cachedOutputs[port] = subGraph.GetAdditionalOutput(i);
            ++i;
        }
    }
    
}
