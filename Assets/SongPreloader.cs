using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

public class IntEvent : UnityEvent<int> {
    public int val = 0;
    public void Invoke() {
        this.Invoke(val);
    }
}


[RequireComponent(typeof(VinylMenuController))]
public class SongPreloader : MonoBehaviour
{
    public MainMenuController mainMenu;

    private GlobalConfiguration config;

    [Inject]
    public void Construct(GlobalConfiguration config)
    {
        this.config = config;
    }

    // Start is called before the first frame update
    void Start()
    {
        var controller = GetComponent<VinylMenuController>();
        var entries = controller.entries;
        for (int i = config.GetLevelCount()-1; i>=0 ; i--) {
            var level = config.GetLevel(i);
            VinylMenuController.MenuEntry entry;
            entry.menuText = level.DisplayName;
            IntEvent evt = new IntEvent();
            evt.val = i;
            evt.AddListener(mainMenu.PlayLevel);
            entry.onSelect = new UnityEvent();
            entry.onSelect.AddListener(new UnityAction(evt.Invoke));
            entries.Add(entry);
            Debug.Log("log");
        }
        controller.UpdateEntries();
    }
}
