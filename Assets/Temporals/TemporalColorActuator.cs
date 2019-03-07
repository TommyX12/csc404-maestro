using System;
using System.Collections;
using System.Collections.Generic;

using Zenject;

using UnityEngine;
using UnityEngine.Assertions;

public class TemporalColorActuator : MonoBehaviour {
    
    // exposed parameters
    public Color baseColor = Color.gray;
    public Color audioColor = Color.gray;
    public string graphOutputName;

    // references
    public Renderer targetRenderer;
    public Material material;

    // injected references
    protected TemporalNodeGraph graph;
    protected GlobalConfiguration config;
    
    // private parameters
    private int colorID;
    private MaterialPropertyBlock block;
    
    public TemporalColorActuator() {
        
    }

    [Inject]
    public void Construct(TemporalNodeGraph graph,
                          GlobalConfiguration config) {
        this.graph = graph;
        this.config = config;
    }
    
    protected void Awake() {
        Assert.IsNotNull(targetRenderer);
        Assert.IsNotNull(graphOutputName);

        if (!material) {
            material = targetRenderer.material;
        }
    }

    protected void Update() {
        float value = ((float) graph.GetAdditionalOutput(graphOutputName));
        material.color = baseColor + value * (audioColor - baseColor);
    }
}
