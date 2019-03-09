using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeController : TemporalController
{
    protected TemporalNodeGraph graph;

    public override void Determine(DeterministicObject obj, float time)
    {
        // Should pass in the obj. to your graph.
        // obj is the object being changed
        // time is global_time - global_start_time
        // starts at 0
    }

    public override void Initialize(DeterministicObject obj)
    {
        // probably leave this empty. Initialization may depend on the graph itself?
        // Might need an initializer Graph
    }
}
