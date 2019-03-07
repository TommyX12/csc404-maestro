using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

[CreateAssetMenu]
public class TemporalNodeGraph : NodeGraph { 

    protected bool initCalled = false;

    protected List<TemporalNodeBase> temporalNodes;

    protected Dictionary<int, AdditionalOutputNode> additionalOutputsByIndex;
    protected Dictionary<string, AdditionalOutputNode> additionalOutputsByName;
    protected Dictionary<int, ArgumentNode> argumentsByIndex;
    protected Dictionary<string, ArgumentNode> argumentsByName;
    protected InputTimeNode inputTime;
    protected OutputTimeNode outputTime;
    protected OutputValueNode outputValue;

    protected float bpm = 60;
    protected float beatLength = 1.0f;

    public TemporalNodeGraph() {
        
    }

    public void SetBPM(float value) {
        if (CheckNotInitialized()) return;
        bpm = value;
        beatLength = 60.0f / bpm;
        foreach (TemporalNodeBase node in temporalNodes) {
            node.SetBPM(value);
        }
    }

    public float GetBPM() {
        return bpm;
    }

    public void Init(float bpm) {
        InitNodes();
        InvalidateCache();
        initCalled = true;
        
        SetBPM(bpm);
    }

    public float GetBeats(float time) {
        return time / beatLength;
    }

    public void InvalidateCache() {
        foreach (TemporalNodeBase node in temporalNodes) {
            node.InvalidateCache();
        }
    }

    public object GetAdditionalOutput(int index, bool invalidateCache = false) {
        if (CheckNotInitialized()) return 0;
        if (invalidateCache) InvalidateCache();
        
        return additionalOutputsByIndex[index].GetValue();
    }

    public object GetAdditionalOutput(string name, bool invalidateCache = false) {
        if (CheckNotInitialized()) return 0;
        if (invalidateCache) InvalidateCache();
        
        return additionalOutputsByName[name].GetValue();
    }

    public void SetArgument(int index, float value, bool invalidateCache = false) {
        if (CheckNotInitialized()) return;
        if (invalidateCache) InvalidateCache();
        
        argumentsByIndex[index].SetValue(value);
    }

    public void SetArgument(string name, float value, bool invalidateCache = false) {
        if (CheckNotInitialized()) return;
        if (invalidateCache) InvalidateCache();
        
        argumentsByName[name].SetValue(value);
    }

    public void SetInputTime(float time, bool invalidateCache = false) {
        if (CheckNotInitialized()) return;
        if (!inputTime) Debug.LogError("No inputTime node in graph.");
        if (invalidateCache) InvalidateCache();
        
        inputTime.SetTime(time);
    }
    
    public object GetOutputTime(bool invalidateCache = false) {
        if (CheckNotInitialized()) return 0;
        if (!outputTime) Debug.LogError("No outputTime node in graph.");
        if (invalidateCache) InvalidateCache();
        
        return outputTime.GetValue();
    }
    
    public object GetOutputValue(bool invalidateCache = false) {
        if (CheckNotInitialized()) return 0;
        if (!outputValue) Debug.LogError("No outputValue node in graph.");
        if (invalidateCache) InvalidateCache();
        
        return outputValue.GetValue();
    }
    
    protected void InitNodes() {
        temporalNodes = new List<TemporalNodeBase>();

        additionalOutputsByIndex = new Dictionary<int, AdditionalOutputNode>();
        additionalOutputsByName = new Dictionary<string, AdditionalOutputNode>();
        argumentsByIndex = new Dictionary<int, ArgumentNode>();
        argumentsByName = new Dictionary<string, ArgumentNode>();
        inputTime = null;
        outputTime = null;
        outputValue = null;
        
        for (int i = 0; i < nodes.Count; i++) {
            var baseNode = nodes[i];
            {
                var node = baseNode as TemporalNodeBase;
                if (node) {
                    temporalNodes.Add(node);
                    node.SetParentGraph(this);
                }
            }
            {
                var node = baseNode as AdditionalOutputNode;
                if (node) {
                    additionalOutputsByIndex[node.index] = node;
                    additionalOutputsByName[node.name] = node;
                }
            }
            {
                var node = baseNode as ArgumentNode;
                if (node) {
                    argumentsByIndex[node.index] = node;
                    argumentsByName[node.name] = node;
                }
            }
            {
                var node = baseNode as InputTimeNode;
                if (node) {
                    inputTime = node;
                }
            }
            {
                var node = baseNode as OutputTimeNode;
                if (node) {
                    outputTime = node;
                }
            }
            {
                var node = baseNode as OutputValueNode;
                if (node) {
                    outputValue = node;
                }
            }
        }
        
    }

    protected bool CheckNotInitialized() {
        if (initCalled) {
            return false;
        }
        else {
            Debug.LogError("Graph not initialized. Please call Init() (such as using a TemporalGraphRunner).");
            return true;
        }
    }
	
}
