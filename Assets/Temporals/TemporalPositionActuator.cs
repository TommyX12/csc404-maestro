using System;
using System.Collections;
using System.Collections.Generic;

using Zenject;

using UnityEngine;
using UnityEngine.Assertions;

public class TemporalPositionActuator : MonoBehaviour {

    // exposed parameters
    public string graphOutputName;

    // refereces
    public Transform targetTransform;

    // injected references
    protected TemporalNodeGraph graph;
    protected GlobalConfiguration config;
    
    public TemporalPositionActuator() {
        
    }

    [Inject]
    public void Construct(TemporalNodeGraph graph,
                          GlobalConfiguration config) {
        this.graph = graph;
        this.config = config;
    }

    protected void Awake() {
        Assert.IsNotNull(graphOutputName);
        Assert.IsNotNull(targetTransform);
    }

    protected void Start() {
        
    }

    protected void Update() {
        float value = ((float) graph.GetAdditionalOutput(graphOutputName));
        Vector3 position = targetTransform.localPosition;
        position.y = value;
        targetTransform.localPosition = position;
    }
}
