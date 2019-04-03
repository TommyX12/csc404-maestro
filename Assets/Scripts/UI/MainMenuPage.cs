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

    public virtual void OnPageEnabled() {
        if (selectedItem)
        {
            selectedItem.Select();
        }
    }

    public virtual void OnPageDisabled() {
        if (EventSystem.current && EventSystem.current.currentSelectedGameObject)
        {
            selectedItem = EventSystem.current.currentSelectedGameObject.GetComponent<Selectable>();
        }
    }
}
