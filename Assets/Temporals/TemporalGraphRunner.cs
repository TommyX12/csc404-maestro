using System;
using System.Collections;
using System.Collections.Generic;

using Zenject;

using UnityEngine;

public class TemporalGraphRunner : MonoBehaviour {

    protected TemporalNodeGraph graph;
    protected GlobalConfiguration config;
    protected MusicManager musicManager;
    
    public TemporalGraphRunner() {
        
    }

    [Inject]
    public void Construct(TemporalNodeGraph graph,
                          GlobalConfiguration config,
                          MusicManager musicManager) {
        this.graph = graph;
        this.config = config;
        this.musicManager = musicManager;
    }

    protected void Awake() {
        graph.Init(config.GetBPM());
    }

    protected void Start() {
        
    }

    protected void Update() {
        graph.InvalidateCache();
        graph.SetInputTime(musicManager.GetTotalTimer());
        // Debug.Log("graph.GetOutputTime() = " + graph.GetOutputTime());
        // Debug.Log("graph.GetOutputValue() = " + graph.GetOutputValue());
        // Debug.Log("graph.GetOutputValue2() = " + graph.GetAdditionalOutput("hello"));
    }
}
