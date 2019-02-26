using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {

    // exposed parameters
    public RectTransform healthBarBarPrefab;
    
    protected int numBars = 8;
    protected float spacing = 0.1f;
    protected float minHeight = 0.5f;
    protected float maxHeight = 1.0f;

    protected RectTransform[] bars;
    
    public HealthBar() {
        
    }

    protected void Awake() {
        if (!healthBarBarPrefab) {
            Debug.Log("healthBarBarPrefab is not injected.");
        }
        
        GenerateBars();
    }

    protected void GenerateBars() {
        bars = new RectTransform[numBars];
        
        float width = numBars > 0 ? (1 - spacing) / numBars : 1;
        float space = numBars > 0 ? spacing / (numBars - 1) : 0;
        float minX = 0;
        float maxX = minX + width;
        for (int i = 0; i < numBars; ++i) {
            RectTransform bar =
                GameObject.Instantiate(healthBarBarPrefab, transform);

            bar.anchorMin = new Vector2(minX, 0);
            bar.anchorMax = new Vector2(maxX, 1);

            minX += width + space;
            maxX += width + space;
            
            bars[i] = bar;
        }
    }

    public int GetNumBars() {
        return numBars;
    }

    public void SetBarHeight(int barIndex, float height) {
        var bar = bars[barIndex];
        Vector2 anchorMin = bar.anchorMin;
        Vector2 anchorMax = bar.anchorMax;
        anchorMax.y = Mathf.Lerp(minHeight, maxHeight, height);
        bar.anchorMin = anchorMin;
        bar.anchorMax = anchorMax;
    }

    protected void Start() {
        
    }

    protected void Update() {
        
    }
}

