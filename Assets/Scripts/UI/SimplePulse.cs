using System;
using System.Collections;
using System.Collections.Generic;

using Zenject;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;

public class SimplePulse : MonoBehaviour {

    // Dragged references
    [SerializeField]
    private Image image;
    [SerializeField]
    private float minValue = 0.5f;
    [SerializeField]
    private float maxValue = 1.0f;
    [SerializeField]
    private float cycle = 1.0f; // number of cycles per beat
    [SerializeField]
    private float phase = 0.0f;
    [SerializeField]
    private float sharpness = 2.0f;

    // Injected references
    private MusicManager musicManager;
    
    protected void Awake() {
        Assert.IsNotNull(image);
    }

    [Inject]
    public void Construct(MusicManager musicManager) {
        this.musicManager = musicManager;
    }

    protected void Start() {
        
    }

    protected void Update() {
        Color color = image.color;

        float time = musicManager.TimeToBeat(musicManager.GetTotalTimer());
        float value = Mathf.Cos(((time * (2 * Mathf.PI)) - phase) / cycle) * 0.5f + 0.5f;
        int sign = value > 0 ? 1 : -1;
        value = sign * Mathf.Pow(Mathf.Abs(value), sharpness);
        value = value * (maxValue - minValue) + minValue;
        color.a = value;
        
        image.color = color;
    }
}
