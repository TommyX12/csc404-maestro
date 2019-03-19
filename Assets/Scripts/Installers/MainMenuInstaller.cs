using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Assertions;

using Zenject;

public class MainMenuInstaller : MonoInstaller {

    [SerializeField]
    private GlobalConfiguration globalConfiguration;
    [SerializeField]
    private MainMenuModel mainMenuModel;
    [SerializeField]
    private GameplayModel gameplayModel;
    [SerializeField]
    private MusicManager musicManager;
    
    public override void InstallBindings() {
        if (globalConfiguration == null) {
            globalConfiguration = ScriptableObject.CreateInstance<GlobalConfiguration>();
        }
        Container.Bind<GlobalConfiguration>()
            .FromInstance(globalConfiguration);

        if (mainMenuModel == null) {
            mainMenuModel = ScriptableObject.CreateInstance<MainMenuModel>();
        }
        Container.Bind<MainMenuModel>()
            .FromInstance(mainMenuModel);

        if (gameplayModel == null) {
            gameplayModel = ScriptableObject.CreateInstance<GameplayModel>();
        }
        Container.Bind<GameplayModel>()
            .FromInstance(gameplayModel);

        Assert.IsNotNull(musicManager);
        Container.Bind<MusicManager>()
            .FromInstance(musicManager);

    }

}
