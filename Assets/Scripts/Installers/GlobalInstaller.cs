using UnityEngine;
using UnityEngine.Assertions;
using Zenject;

public class GlobalInstaller : MonoInstaller
{

    private GameplayModel gameplayModel;
    private GlobalConfiguration globalConfiguration;
    private GlobalRules globalRules;
    private MusicManager musicManager;

    // Dragged references
    public CombatGameManager gameManager;
    public RectTransform healthBarBarPrefab;
    
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

        musicManager = MusicManager.current;
        Container.Bind<MusicManager>()
            .FromInstance(musicManager);

        Assert.IsNotNull(gameManager);
        Container.Bind<CombatGameManager>()
            .FromInstance(gameManager);
        
        Assert.IsNotNull(healthBarBarPrefab);
        Container.Bind<RectTransform>()
            .WithId(Constants.Prefab.HEALTH_BAR_BAR)
            .FromInstance(healthBarBarPrefab);
    }
}
