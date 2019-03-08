using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using XNode;

[NodeWidth(350)]
[NodeTint("#aaffaa")]
public abstract class SubgraphNodeBase : TemporalNodeBase {

    public TemporalNodeGraph subGraph;

    public SubgraphNodeBase() {
        
    }

    public override void SetBPM(float value) {
        base.SetBPM(value);
        subGraph.SetBPM(value);
    }

    protected override void Init() {
        if (subGraph) {
            subGraph.Init(GetBPM());
        }
        else {
            Debug.LogError("Sub-graph instance is null. Please link sub-graph instance.");
        }
    }

}
