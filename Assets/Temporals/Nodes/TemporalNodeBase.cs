using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using XNode;

public abstract class TemporalNodeBase : Node {

    protected TemporalNodeGraph parentGraph;

    protected bool cacheDirty = true;
    protected Dictionary<NodePort, object> cachedOutputs = new Dictionary<NodePort, object>();

    protected float bpm = 60;
    protected float beatLength = 1.0f;

    public TemporalNodeBase() {
        
    }

    public float GetBeats(float time) {
        return time / beatLength;
    }

    public float GetTime(float beats) {
        return beats * beatLength;
    }

    public virtual void SetBPM(float value) {
        bpm = value;
        beatLength = 60.0f / bpm;
    }

    public float GetBPM() {
        return bpm;
    }

    public void SetParentGraph(TemporalNodeGraph graph) {
        parentGraph = graph;
    }

    public TemporalNodeGraph GetParentGraph() {
        return parentGraph;
    }

    public void InvalidateCache() {
        cacheDirty = true;
        OnInvalidateCache();
    } 

    protected virtual void OnInvalidateCache() {
        // override
    }

    public void CacheOutputs() {
        ComputeCache();
        cacheDirty = false;
    }

    protected abstract void ComputeCache();

    protected void SetCachedOutput(string outputName, object value) {
        cachedOutputs[GetOutputPort(outputName)] = value;
    }
    
    public sealed override object GetValue(NodePort port) {
        if (cacheDirty) CacheOutputs();
        object result;
        cachedOutputs.TryGetValue(port, out result);
        return result;
    }

    public object GetValue(string name) {
        return GetValue(GetOutputPort(name));
    }

}
