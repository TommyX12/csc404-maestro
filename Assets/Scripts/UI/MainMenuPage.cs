using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MainMenuPage : MonoBehaviour {

    [SerializeField]
    private Selectable selectedItem;
    
    private void Awake() {
        
    }

    private void Start() {
        
    }

    private void Update() {
        
    }

    public void OnPageEnabled() {
        selectedItem.Select();
    }

    public void OnPageDisabled() {
        selectedItem = EventSystem.current.currentSelectedGameObject.GetComponent<Selectable>();
    }
}
