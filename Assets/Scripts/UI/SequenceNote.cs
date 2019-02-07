using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class SequenceNote : MonoBehaviour{

    private Color idleColor = new Color(1.0f, 1.0f, 1.0f, 0.5f);
    private Color excitedColor = new Color(0.4f, 0.8f, 1.0f, 1.0f);
    private Vector3 idleScale = new Vector3(1.0f, 1.0f, 1.0f);
    private Vector3 excitedScale = new Vector3(1.2f, 1.2f, 1.0f);

    private Image image;
    private RectTransform rectTransform;

    public Riff.Note note;
    
    public SequenceNote() {
        
    }

    protected void Awake() {
        image = GetComponent<Image>();
        rectTransform = GetComponent<RectTransform>();
    }

    protected void Start() {
        
    }

    public void SetPosition(float position) {
        Vector2 anchorMin = rectTransform.anchorMin;
        Vector2 anchorMax = rectTransform.anchorMax;
        anchorMin.x = anchorMax.x = position;
        rectTransform.anchorMin = anchorMin;
        rectTransform.anchorMax = anchorMax;
    }

    public void SetExcitement(float distanceToBeat) {
        distanceToBeat = -distanceToBeat;
        float colorExcitement = Mathf.Min(Mathf.Max(1 / (10.0f * distanceToBeat), 0.0f), 1.0f);
        float scaleExcitement = Mathf.Min(Mathf.Max(1 / (10.0f * Mathf.Abs(distanceToBeat)), 0.0f), 1.0f);
        
        var color = image.color;
        color = idleColor + colorExcitement * (excitedColor - idleColor);
        image.color = color;
        
        var scale = rectTransform.localScale;
        scale = idleScale + scaleExcitement * (excitedScale - idleScale);
        rectTransform.localScale = scale;
    }

    protected void Update() {
        
    }
}
