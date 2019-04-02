using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;

using Zenject;

public class PowerupUIController : MonoBehaviour {

    [SerializeField]
    private Image timeLeftBarImage;
    [SerializeField]
    private RectTransform container;

    // Injected references
    private GameplayModel model;
    private GlobalConfiguration config;

    // Fields
    private float targetAnchorValue = 0;

    private void Awake() {
        Assert.IsNotNull(timeLeftBarImage, "timeLeftBarImage");
        Assert.IsNotNull(container, "container");
        Hide(/* immediate */ true);
    }

    [Inject]
    public void Construct(GameplayModel model,
                          GlobalConfiguration config) {
        this.model = model;
        this.config = config;
    }

    private void Hide(bool immediate = false) {
        targetAnchorValue = 1;

        if (immediate) {
            Vector2 anchorMin = container.anchorMin;
            Vector2 anchorMax = container.anchorMax;
            anchorMin.x = targetAnchorValue;
            anchorMax.x = targetAnchorValue + 1;
            container.anchorMin = anchorMin;
            container.anchorMax = anchorMax;
        }
    }

    private void Show() {
        targetAnchorValue = 0;
    }

    private void OnPickupEventActivated() {
        Show();
    }

    private void OnPickupEventEnded() {
        Hide();
    }

    private void Start() {
        model.PickupEffectActivated += OnPickupEventActivated;
        model.PickupEffectEnded += OnPickupEventEnded;
    }

    private void OnDestroy() {
        model.PickupEffectActivated -= OnPickupEventActivated;
        model.PickupEffectEnded -= OnPickupEventEnded;
    }

    private void Update() {
        timeLeftBarImage.fillAmount = model.CurrentPickupTimeLeftPercentage;

        float lerpFactor = config.PowerupUILerpFactor;

        Vector2 anchorMin = container.anchorMin;
        Vector2 anchorMax = container.anchorMax;
        float targetAnchorMin = targetAnchorValue;
        float targetAnchorMax = targetAnchorValue + 1;
        anchorMin.x = Mathf.Lerp(anchorMin.x, targetAnchorMin, lerpFactor);
        anchorMax.x = Mathf.Lerp(anchorMax.x, targetAnchorMax, lerpFactor);
        container.anchorMin = anchorMin;
        container.anchorMax = anchorMax;
    }
}
