using System;
using System.Collections;
using System.Collections.Generic;

using Zenject;

using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {

    protected int numBars = 8;
    protected float spacing = 0.1f;
    protected float minHeight = 0.5f;
    protected float maxHeight = 1.0f;

    public Color aliveColor;
    public Color deadColor;

    protected RectTransform[] bars;
    
    public HealthBar() {
        
    }

    // Injected references
    private RectTransform healthBarBarPrefab;
    private GameplayModel model;

    [Inject]
    public void Construct([Inject(Id = Constants.Prefab.HEALTH_BAR_BAR)]
                          RectTransform healthBarBarPrefab,
                          GameplayModel model) {
        this.healthBarBarPrefab = healthBarBarPrefab;
        this.model = model;
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

            bar.GetComponent<ScaleToBand>().band = i;
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
        var playerHealth = model.PlayerHealth;
        var playerTotalHealth = 8;
        int numBarsActive = Math.Min(Math.Max(Mathf.CeilToInt(playerHealth / playerTotalHealth * numBars), 0), numBars);
        for (int i = 0; i < bars.Length; ++i) {
            Color color = i < numBarsActive ? aliveColor : deadColor;
            bars[i].gameObject.GetComponent<Image>().color = color;
        }
    }
}

