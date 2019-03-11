using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using UnityEngine.Assertions;

using Zenject;

public class TutorialUI : MonoBehaviour {

    // Injected references
    private GlobalConfiguration config;
    private GameplayModel model;

    // Self references
    [SerializeField]
    private Text uiText;

    // Fields
    private float timeOut;

    [Inject]
    public void Construct(GlobalConfiguration config,
                          GameplayModel model) {
        this.config = config;
        this.model = model;
    }
    
    protected void Awake() {
        Assert.IsNotNull(uiText);
        model.TutorialFinished += OnTutorialFinished;
        model.ShowMoveTutorial += OnShowMoveTutorial;
    }

    private void OnTutorialFinished() {
        Hide();
    }

    private void OnShowMoveTutorial() {
        ShowText(config.MoveTutorialText, config.MoveTutorialTimeout);
    }

    protected void Start() {
        
    }

    protected void Update() {
        if (timeOut > 0) {
            timeOut -= Time.deltaTime;
            if (timeOut <= 0) {
                Hide();
            }
        }
    }

    public void ShowText(string text, float timeOut = 0) {
        if (timeOut > 0) {
            this.timeOut = timeOut;
        }
        uiText.text = text;
        gameObject.SetActive(true);
    }

    public void Hide() {
        gameObject.SetActive(false);
    }

    protected void OnDestroy() {
        model.TutorialFinished -= OnTutorialFinished;
    }
}
