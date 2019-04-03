using UnityEngine;
using UnityEngine.Assertions;
using Zenject;

public class GlobalInstaller : MonoInstaller
{

    [SerializeField]
    private CombatGameManager gameManager;
    [SerializeField]
    private RectTransform healthBarBarPrefab;
    [SerializeField]
    private GlobalConfiguration globalConfiguration;
    [SerializeField]
    private GameplayModel gameplayModel;
    [SerializeField]
    private GlobalRules globalRules;
    [SerializeField]
    private MusicManager musicManager;
    [SerializeField]
    private PickupManager pickupManager;
    [SerializeField]
    private PlayerAgentController player;
    [SerializeField]
    private PrefabObjectProvider prefabProvider;
    [SerializeField]
    private LevelConfiguration levelConfiguration;

    public override void InstallBindings()
    {
        if (gameplayModel == null) {
            gameplayModel = ScriptableObject.CreateInstance<GameplayModel>();
        }
        Container.Bind<GameplayModel>()
            .FromInstance(gameplayModel);

        if (globalConfiguration == null) {
            globalConfiguration = ScriptableObject.CreateInstance<GlobalConfiguration>();
        }
        Container.Bind<GlobalConfiguration>()
            .FromInstance(globalConfiguration);

        if (globalRules == null) {
            globalRules = new GlobalRules(globalConfiguration);
        }
        Container.Bind<GlobalRules>()
            .FromInstance(globalRules);

        Assert.IsNotNull(musicManager);
        Container.Bind<MusicManager>()
            .FromInstance(musicManager);

        Assert.IsNotNull(pickupManager);
        Container.Bind<PickupManager>()
            .FromInstance(pickupManager);

        Assert.IsNotNull(gameManager);
        Container.Bind<CombatGameManager>()
            .FromInstance(gameManager);

        Assert.IsNotNull(prefabProvider);
        Container.Bind<PrefabObjectProvider>()
            .FromInstance(prefabProvider);
        
        Assert.IsNotNull(healthBarBarPrefab);
        Container.Bind<RectTransform>()
            .WithId(Constants.Prefab.HEALTH_BAR_BAR)
            .FromInstance(healthBarBarPrefab);

        if (player == null) {
            player = gameManager.player;
        }
        Container.Bind<PlayerAgentController>()
            .FromInstance(player);

        Assert.IsNotNull(levelConfiguration);
        Container.Bind<LevelConfiguration>()
            .FromInstance(levelConfiguration);
    }
}
