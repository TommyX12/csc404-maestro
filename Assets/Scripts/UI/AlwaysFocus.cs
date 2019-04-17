using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Assertions;

public class AlwaysFocus : MonoBehaviour {

    [SerializeField]
    private Selectable selectable;

    private void Awake() {
        if (!selectable) {
            selectable = GetComponent<Selectable>();
        }
        Assert.IsNotNull(selectable, "GameObject has no Selectable component");
    }

    private void Update() {
        if (EventSystem.current.currentSelectedGameObject != gameObject) {
            EventSystem.current.SetSelectedGameObject(gameObject);
            selectable.Select();
            selectable.OnSelect(null);
        }
    }

}
