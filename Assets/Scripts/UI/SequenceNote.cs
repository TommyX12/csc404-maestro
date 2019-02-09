using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class SequenceNote : MonoBehaviour{

    public const int HIT_STATE_MISS = 0;
    public const int HIT_STATE_HIT = 1;

    private Color idleColor = new Color(1.0f, 1.0f, 1.0f, 0.75f);
    private Color missColor = new Color(1.0f, 0.3f, 0.3f, 0.75f);
    private Color hitColor = new Color(0.2f, 1.0f, 0.2f, 1.0f);
    private Vector3 idleScale = new Vector3(1.0f, 1.0f, 1.0f);
    private Vector3 missScale = new Vector3(1.1f, 1.1f, 1.0f);
    private Vector3 hitScale = new Vector3(1.5f, 1.5f, 1.0f);

    // references
    public Image image;
    public RectTransform notePanel;
    
    private RectTransform rectTransform;

    private float hitEffectTimer = 0;
    private float hitEffectDuration= 1.0f;

    public Riff.Note note;
    
    public SequenceNote() {
        
    }

    protected void Awake() {
        // image = GetComponent<Image>();
        rectTransform = GetComponent<RectTransform>();
    }

    protected void Start() {
        
    }

    public void SetPosition(float position) {
        Vector3 eulerAngles = rectTransform.eulerAngles;
        eulerAngles.z = -360.0f * position;
        rectTransform.eulerAngles = eulerAngles;
    }

    public void SetBeatDistance(float distanceToBeat) {
        Color excitedColor = missColor + Mathf.Min(1.0f, 2 * hitEffectTimer / hitEffectDuration) * (hitColor - missColor);
        Vector3 excitedScale = missScale + Mathf.Min(1.0f, 2 * hitEffectTimer / hitEffectDuration) * (hitScale - missScale);
        
        distanceToBeat = -distanceToBeat;
        float colorExcitement = Mathf.Min(Mathf.Max(1 / (10.0f * distanceToBeat), 0.0f), 1.0f);
        float scaleExcitement = Mathf.Min(Mathf.Max(1 / (10.0f * Mathf.Abs(distanceToBeat)), 0.0f), 1.0f);
        if (distanceToBeat > 0) {
            scaleExcitement *= 2.5f;
        }
        
        var color = image.color;
        color = idleColor + colorExcitement * (excitedColor - idleColor);
        image.color = color;
        
        var scale = notePanel.localScale;
        scale = idleScale + scaleExcitement * (excitedScale - idleScale);
        notePanel.localScale = scale;
    }

    public void SetHitState(int hitState) {
        if (hitState == HIT_STATE_HIT) {
            hitEffectTimer = hitEffectDuration;
        }
        else {
            hitEffectTimer = 0;
        }
    }

    protected void Update() {
        if (hitEffectTimer > 0) {
            hitEffectTimer -= Time.deltaTime;
        }
        if (hitEffectTimer < 0) {
            hitEffectTimer = 0;
        }
    }
}