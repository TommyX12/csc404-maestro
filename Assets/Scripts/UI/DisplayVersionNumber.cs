using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;

using Zenject;

public class DisplayVersionNumber : MonoBehaviour {

    // Injected references
    private GlobalConfiguration config;

    // Self references
    private Text text;
    
    [Inject]
    public void Construct(GlobalConfiguration config) {
        this.config = config;
    }

    private void Awake() {
        text = GetComponent<Text>();
        Assert.IsNotNull(text);
    }

    private void Start() {
        text.text = config.Version;
    }

    private void Update() {
        
    }
}
