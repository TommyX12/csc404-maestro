using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class Crosshair : MonoBehaviour {

    // self reference
    private Image image;
    private RectTransform rectTransform;
    private Canvas canvas;

    protected Vector3 targetPosition = new Vector3(0.5f, 0.5f, 10.0f);
    protected Vector3 crosshairPosition = new Vector3(0.5f, 1.5f, 10.0f);
    protected float positionSmoothFriction = 0.7f;
    
    protected float targetScale = 1.0f;
    protected float currentScale = 1.0f;
    protected float scaleSmoothFriction = 0.7f;
    protected float staticScale = 1.0f;
    
    public Crosshair() {
        
    }

    protected void Awake() {
        image = GetComponent<Image>();
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
    }

    protected void Start() {
        
    }

    protected void FixedUpdate() {
        var player = CombatGameManager.current.player;
        if (player) {
            // transform update
            Vector3 v = new Vector3();
            Agent target = player.GetTarget();
            if (target) {
                targetPosition = Util.WorldToScreenAnchor(canvas, target.transform.position);
                targetScale = 1.0f;
            }
            else {
                targetScale = 0.0f;
            }

            crosshairPosition = Vector3.Lerp(crosshairPosition, targetPosition, 1 - positionSmoothFriction);
            rectTransform.anchorMin = rectTransform.anchorMax = crosshairPosition;

            currentScale = Mathf.Lerp(currentScale, targetScale, 1 - scaleSmoothFriction);
            rectTransform.localScale = new Vector3(currentScale, currentScale, currentScale) * staticScale;
        }
        else {
            targetScale = 0.0f;
        }
    }
}
