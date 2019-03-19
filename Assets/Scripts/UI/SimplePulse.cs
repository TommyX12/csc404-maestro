using System;
using System.Collections;
using System.Collections.Generic;

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
    private float cycle = 1.0f;
    [SerializeField]
    private float phase = 0.0f;
    [SerializeField]
    private float sharpness = 1.0f;
    
    protected void Awake() {
        Assert.IsNotNull(image);
    }

    protected void Start() {
        
    }

    protected void Update() {
        Color color = image.color;

        float time = Time.fixedTime;
        float value = Mathf.Cos(((time * (2 * Mathf.PI)) - phase) / cycle) * 0.5f + 0.5f;
        int sign = value > 0 ? 1 : -1;
        value = sign * Mathf.Pow(Mathf.Abs(value), sharpness);
        value = value * (maxValue - minValue) + minValue;
        color.a = value;
        
        image.color = color;
    }
}
