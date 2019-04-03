using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;

using Zenject;

public class GameplayUIController : MonoBehaviour {

    // Children references
    [SerializeField]
    private Image flashScreenImage;

    // Fields
    private float screenFlashTimer = 0;

    // Injected references
    private GameplayModel model;
    private GlobalConfiguration config;
    
    private void Awake() {
        Assert.IsNotNull(flashScreenImage, "flashScreenImage");
    }

    [Inject]
    public void Construct(GameplayModel model,
                          GlobalConfiguration config) {
        this.model = model;
        this.config = config;
    }

    private void FlashScreen() {
        screenFlashTimer = config.PowerupScreenFlashDuration;
    }

    private void Start() {
        model.PickupEffectActivated += OnPickupEventActivated;
    }

    private void OnDestroy() {
        model.PickupEffectActivated -= OnPickupEventActivated;
    }

    private void OnPickupEventActivated(PickupEffect effect) {
        FlashScreen();
    }

    private void Update() {

        // Update screen flashing image
        screenFlashTimer = Mathf.Max(0, screenFlashTimer - Time.deltaTime);
        Color flashScreenImageColor = flashScreenImage.color;
        flashScreenImageColor.a = config.PowerupScreenFlashIntensity
            * screenFlashTimer / config.PowerupScreenFlashDuration;
        flashScreenImage.color = flashScreenImageColor;

    }
}
