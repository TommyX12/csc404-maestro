using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

using Zenject;

public class VinylMenuController : MainMenuPage
{

    public delegate void OnStateChange();
    event OnStateChange OnChange;

    [Serializable]
    public struct MenuEntry {
        public string menuText;
        public UnityEvent onSelect;
    }

    public GlobalConfiguration config;

    [Inject]
    public void Construct(GlobalConfiguration config)
    {
        this.config = config;
    }

    public List<MenuEntry> entries;

    public VinylMenuTextController textController;

    public AudioSource menuMove;

    public void UpdateEntries() {
        textController.SetMenuItems(entries);
    }

    private void Start()
    {
        UpdateEntries();
    }

    private void Update()
    {
                                                           
        if (Input.GetKeyDown(KeyCode.S) || ControllerProxy.GetVerticalAxisOnce(true) == -1) {
            textController.ScrollDown();
            menuMove.Play();
            OnChange();
        }

        if (Input.GetKeyDown(KeyCode.W) || ControllerProxy.GetVerticalAxisOnce(true) == 1) {
            textController.ScrollUp();
            menuMove.Play();
            OnChange();
        }

        if (Input.GetKeyDown(KeyCode.Return) || Input.GetButtonDown("Fire1")) {
            entries[textController.absoluteSelection].onSelect.Invoke();
        }
    }

    public override void OnPageDisabled()
    {
    }

    public override void OnPageEnabled()
    {
    }
}
