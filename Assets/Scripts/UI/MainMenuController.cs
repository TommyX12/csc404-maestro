using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

using Zenject;

public class MainMenuController : MonoBehaviour
{

    // Exposed references
    [SerializeField]
    private MainMenuPage currentPage;
    
    // Injected references
    private MainMenuModel model;
    private GlobalConfiguration config;
    private MusicManager musicManager;

    [Inject]
    public void Construct(MainMenuModel model,
                          GlobalConfiguration config,
                          MusicManager musicManager) {
        this.model = model;
        this.config = config;
        this.musicManager = musicManager;
    }

    private void Awake() {
        Assert.IsNotNull(currentPage);
        musicManager.StartRiff("white-noise", 120);
    }

    private void Start() {
        SwitchToPage(currentPage);
    }

    public void SwitchToPage(MainMenuPage page) {
        if (page != currentPage) {
            currentPage.OnPageDisabled();
            currentPage.gameObject.SetActive(false);
        }
        currentPage = page;
        currentPage.gameObject.SetActive(true);
        currentPage.OnPageEnabled();
    }

    public void PlayLevel(int index) {
        var level = config.GetLevel(index);
        ControllerProxy.inputEnabled = true; // enable input before level loads
        SceneManager.LoadScene(level.SceneName);
    }

    public void Exit() {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
          Application.Quit();
#endif
    }
}
