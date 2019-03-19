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
        musicManager.StartRiff("main-menu-2", 120);
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
        SceneManager.LoadScene(level.SceneName);
    }

    private void Update()
    {
        // if (ControllerProxy.GetButton("Fire1")) {
        //     SceneManager.LoadScene(sceneToLoad);
        // }
    }
}
