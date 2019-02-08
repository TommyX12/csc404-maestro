using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class SequenceMarker : MonoBehaviour{

    private RectTransform rectTransform;
    
    public SequenceMarker() {
        
    }

    protected void Awake() {
        rectTransform = GetComponent<RectTransform>();
    }

    protected void Start() {
        
    }

    protected void Update() {
        
    }
    
    public void SetPosition(float position) {
        Vector3 eulerAngles = rectTransform.eulerAngles;
        eulerAngles.z = -360.0f * position;
        rectTransform.eulerAngles = eulerAngles;
    }
}
