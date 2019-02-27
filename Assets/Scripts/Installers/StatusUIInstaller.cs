using UnityEngine;
using UnityEngine.Assertions;
using Zenject;

public class StatusUIInstaller : MonoInstaller
{

    public CombatGameManager gameManager;
    public RectTransform healthBarBarPrefab;
    
    public override void InstallBindings()
    {
        Assert.IsNotNull(gameManager);
        Container.Bind<CombatGameManager>()
            .FromInstance(gameManager);
        
        Assert.IsNotNull(healthBarBarPrefab);
        Container.Bind<RectTransform>()
            .WithId(Constants.Prefab.HEALTH_BAR_BAR)
            .FromInstance(healthBarBarPrefab);
    }
}
