using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Assertions;

using Zenject;

public class PickupManager : MonoBehaviour {

    [SerializeField]
    private BoxCollider spawnArea;

    // Injected references
    private PrefabObjectProvider prefabProvider;
    private DiContainer diContainer;
    private GlobalConfiguration config;
    private MusicManager musicManager;
    private GameplayModel model;

    private float spawnTimer = 0;
    private bool pickupOnStage = false;
    private bool pickupEffectActive = false;
    private float pickupEffectTimerBeats = 0;
    private PickupEffect activeEffect = null;
    private float lastMusicBeat = 0;
    
    private void Awake() {
        Assert.IsNotNull(spawnArea, "pickup manager requires a BoxCollider as spawnArea");
    }

    [Inject]
    public void Construct(PrefabObjectProvider prefabProvider,
                          MusicManager musicManager,
                          DiContainer diContainer,
                          GlobalConfiguration config,
                          GameplayModel model) {
        this.prefabProvider = prefabProvider;
        this.diContainer = diContainer;
        this.config = config;
        this.musicManager = musicManager;
        this.model = model;
    }

    public void StartEffect(PickupEffect effect) {
        activeEffect = effect;
        model.CurrentPickupEffect = effect;
        pickupEffectActive = true;
        pickupEffectTimerBeats = config.PickupEffectDuration;
        model.NotifyPickupEffectActivated(effect);
    }

    public void EndEffect() {
        activeEffect = null;
        model.CurrentPickupEffect = null;
        pickupEffectActive = false;
        model.NotifyPickupEffectEnded();
    }

    public void NotifyPickupDestroyed(Pickup pickup) {
        pickupOnStage = false;
    }

    public float GetEffectTimeLeftPercentage() {
        return pickupEffectTimerBeats / config.PickupEffectDuration;
    }

    private void Start() {

    }

    private void Update() {

        float musicBeat = musicManager.TimeToBeat(musicManager.GetTotalTimer());
        float deltaBeats = Mathf.Max(0, musicBeat - lastMusicBeat);
        lastMusicBeat = musicBeat;

        spawnTimer += deltaBeats;
        if (spawnTimer > config.PickupSpawnInterval) {
            spawnTimer = 0;
            if (!(pickupOnStage || pickupEffectActive) && model.CanSpawnPickup) {
                SpawnRandomPickup();
            }
        }

        if (pickupEffectActive) {
            pickupEffectTimerBeats -= deltaBeats;
            if (pickupEffectTimerBeats <= 0) {
                EndEffect();
            }
        }

        model.CurrentPickupTimeLeftPercentage = GetEffectTimeLeftPercentage();
    }

    private void SpawnRandomPickup() {
        var prefab = prefabProvider.GetPickup();
        var gameObject = diContainer.InstantiatePrefab(prefab);
        gameObject.transform.position = GetRandomSpawnPosition();
        gameObject.GetComponent<Pickup>().Initialize
            (this,
             musicManager.BeatToTime(config.PickupItemDuration),
             PickupEffect.GetRandomPickupEffect());
        pickupOnStage = true;
    }

    private Vector3 GetRandomSpawnPosition() {
        var bounds = spawnArea.bounds;
        float x = UnityEngine.Random.Range(bounds.min.x, bounds.max.x);
        float y = UnityEngine.Random.Range(bounds.min.y, bounds.max.y);
        float z = UnityEngine.Random.Range(bounds.min.z, bounds.max.z);
        var position = new Vector3(x, y, z);
        return position;
    }
}
