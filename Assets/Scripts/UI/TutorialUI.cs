using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Zenject;

public class TutorialUI : MonoBehaviour {

    // Injected references
    private GameplayModel model;

    [Inject]
    public void Construct(GameplayModel model) {
        this.model = model;
    }
    
    protected void Awake() {
        model.TutorialFinished += OnTutorialFinished;
    }

    private void OnTutorialFinished() {
        gameObject.SetActive(false);
    }

    protected void Start() {
        
    }

    protected void Update() {
        
    }

    protected void OnDestroy() {
        model.TutorialFinished -= OnTutorialFinished;
    }
}
