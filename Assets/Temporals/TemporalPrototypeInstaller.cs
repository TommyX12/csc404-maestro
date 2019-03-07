using System;
using System.Collections;
using System.Collections.Generic;

using Zenject;

using UnityEngine;
using UnityEngine.Assertions;

public class TemporalPrototypeInstaller : MonoInstaller {
    
    public TemporalNodeGraph mainGraph;
    // public GlobalConfiguration config;
    public MusicManager musicManager;

    public override void InstallBindings() {
        Assert.IsNotNull(mainGraph);
        Container.Bind<TemporalNodeGraph>()
            .FromInstance(mainGraph);

        // Assert.IsNotNull(config);
        Container.Bind<GlobalConfiguration>()
            .FromInstance(ScriptableObject.CreateInstance<GlobalConfiguration>());

        Assert.IsNotNull(musicManager);
        Container.Bind<MusicManager>()
            .FromInstance(musicManager);
    }
}
