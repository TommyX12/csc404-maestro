using UnityEngine;
using UnityEngine.Assertions;
using Zenject;

public class GlobalInstaller : MonoInstaller
{

    private GameplayModel gameplayModel;
    private GlobalConfiguration globalConfiguration;
    private GlobalRules globalRules;
    
    public override void InstallBindings()
    {
        gameplayModel = ScriptableObject.CreateInstance<GameplayModel>();
        Container.Bind<GameplayModel>()
            .FromInstance(gameplayModel);

        globalConfiguration = ScriptableObject.CreateInstance<GlobalConfiguration>();
        Container.Bind<GlobalConfiguration>()
            .FromInstance(globalConfiguration);

        globalRules = new GlobalRules(globalConfiguration);
        Container.Bind<GlobalRules>()
            .FromInstance(globalRules);
    }
}
