using UnityEngine;
using UnityEngine.Assertions;
using Zenject;

public class GlobalInstaller : MonoInstaller
{

    private GameplayUIModel gameplayUIModel;
    private GlobalConfiguration globalConfiguration;
    private GlobalRules globalRules;
    
    public override void InstallBindings()
    {
        gameplayUIModel = ScriptableObject.CreateInstance<GameplayUIModel>();
        Container.Bind<GameplayUIModel>()
            .FromInstance(gameplayUIModel);

        globalConfiguration = ScriptableObject.CreateInstance<GlobalConfiguration>();
        Container.Bind<GlobalConfiguration>()
            .FromInstance(globalConfiguration);

        globalRules = new GlobalRules(globalConfiguration);
        Container.Bind<GlobalRules>()
            .FromInstance(globalRules);
    }
}
