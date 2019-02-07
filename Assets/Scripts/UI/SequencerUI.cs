using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class SequencerUI : MonoBehaviour{

    public RectTransform pointerBar;

    private MusicManager musicManager;
    
    public SequencerUI() {
        
    }

    protected void Awake() {
        musicManager = MusicManager.Current;
        
        pointerBar.gameObject.SetActive(true);
    }

    protected void Start() {
        
    }

    protected void Update() {
        Vector2 anchorMin = pointerBar.anchorMin;
        Vector2 anchorMax = pointerBar.anchorMax;
        anchorMin.x = anchorMax.x = (musicManager.GetBeatIndex(4)) / 4;
        pointerBar.anchorMin = anchorMin;
        pointerBar.anchorMax = anchorMax;
    }
}

