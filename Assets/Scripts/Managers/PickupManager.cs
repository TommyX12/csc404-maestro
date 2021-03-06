using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Assertions;

using Zenject;

public class PickupManager : MonoBehaviour {

    [SerializeField]
    private BoxCollider spawnArea;

    private GuideLine guideLine = null;

    // Injected references
    private PrefabObjectProvider prefabProvider;
    private DiContainer diContainer;
    private GlobalConfiguration config;
    private MusicManager musicManager;
    private GameplayModel model;
    private PlayerAgentController player;
    private LevelConfiguration levelConfiguration;

    private float spawnTimer = 0;
    private GameObject pickupOnStage = null;
    private bool pickupEffectActive = false;
    private float pickupEffectTimerBeats = 0;
    private PickupEffect activeEffect = null;
    private float lastMusicBeat = 0;
    
    private void Awake() {
        Assert.IsNotNull(spawnArea, "pickup manager requires a BoxCollider as spawnArea");
        spawnArea.enabled = false;
    }

    [Inject]
    public void Construct(PrefabObjectProvider prefabProvider,
                          MusicManager musicManager,
                          DiContainer diContainer,
                          GlobalConfiguration config,
                          GameplayModel model,
                          PlayerAgentController player,
                          LevelConfiguration levelConfiguration) {
        this.prefabProvider = prefabProvider;
        this.diContainer = diContainer;
        this.config = config;
        this.musicManager = musicManager;
        this.model = model;
        this.player = player;
        this.levelConfiguration = levelConfiguration;
    }

    private GuideLine GetGuideLine() {
        if (guideLine == null) {
            guideLine = diContainer.InstantiatePrefab
                (prefabProvider.GetGuideLine())
                .GetComponent<GuideLine>();
            guideLine.gameObject.SetActive(false);
        }
        return guideLine;
    }

    public void SpawnPickupStartParticle(PickupEffect effect) {
        var startParticlePrefab = prefabProvider.GetPickupStartParticle(effect.effectType);
        if (startParticlePrefab) {
            var startParticle = diContainer.InstantiatePrefab(startParticlePrefab);
            var selfDestruct = startParticle.AddComponent<AutoSelfDestruct>();
            selfDestruct.Delay = config.PickupStartParticleDuration;
            var offset = config.PickupStartParticleOffset;
            // if (Camera.main) {
            //     offset = -Camera.main.transform.forward.normalized * config.PickupStartParticleOffsetDistance;
            // }
            // offset.y += 2.0f;
            startParticle.transform.position = player.transform.position + offset;
            startParticle.transform.localScale *= config.PickupStartParticleScale;
        }
    }

    public void StartEffect(PickupEffect effect) {

        // TODO: spawn particle
        SpawnPickupStartParticle(effect);

        var data = new PickupEffect.EffectFunctionData() {
            player = player,
            levelConfiguration = levelConfiguration,
        };
        effect.Start(data);
        activeEffect = effect;
        model.CurrentPickupEffect = effect;
        pickupEffectActive = true;
        pickupEffectTimerBeats = config.PickupEffectDuration;
        model.NotifyPickupEffectActivated(effect);

        musicManager.PlayOnceAligned(config.PickupStartEffectSound, 2, 0);
    }

    public void EndEffect() {
        var data = new PickupEffect.EffectFunctionData() {
            player = player,
            levelConfiguration = levelConfiguration,
        };
        activeEffect.End(data);
        activeEffect = null;
        model.CurrentPickupEffect = null;
        pickupEffectActive = false;
        model.NotifyPickupEffectEnded();
    }

    public void NotifyPickupDestroyed(Pickup pickup) {
        pickupOnStage = null;
    }

    public float GetEffectTimeLeftPercentage() {
        return pickupEffectTimerBeats / config.PickupEffectDuration;
    }

    private void Start() {

    }

    private void UpdateGuideLine() {
        GuideLine guideLine = GetGuideLine();

        if (pickupOnStage == null || player == null) {
            guideLine.gameObject.SetActive(false);
            return;
        }

        guideLine.gameObject.SetActive(true);
        guideLine.SetStart(player.transform.position);
        guideLine.SetEnd(pickupOnStage.transform.position);
    }

    private void Update() {

        float musicBeat = musicManager.TimeToBeat(musicManager.GetTotalTimer());
        float deltaBeats = Mathf.Max(0, musicBeat - lastMusicBeat);
        lastMusicBeat = musicBeat;

        spawnTimer += deltaBeats;
        if (spawnTimer > config.PickupSpawnInterval) {
            spawnTimer = 0;
            if (!((pickupOnStage != null) || pickupEffectActive) && model.CanSpawnPickup) {
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

        UpdateGuideLine();
    }

    private void SpawnRandomPickup() {
        var prefab = prefabProvider.GetPickup();
        var gameObject = diContainer.InstantiatePrefab(prefab);
        var effect = PickupEffect.GetRandomPickupEffect();
        gameObject.transform.position = GetRandomSpawnPosition();
        gameObject.GetComponent<Pickup>().Initialize
            (this,
             prefabProvider.GetPickupItemParticle(effect.effectType),
             musicManager.BeatToTime(config.PickupItemDuration),
             effect);
        pickupOnStage = gameObject;

        musicManager.PlayOnceAligned(config.PickupSpawnedSound, 2, 0);
    }

    private Vector3 GetRandomSpawnPosition() {
        spawnArea.enabled = true;
        var bounds = spawnArea.bounds;
        float x = UnityEngine.Random.Range(bounds.min.x, bounds.max.x);
        float y = UnityEngine.Random.Range(bounds.min.y, bounds.max.y);
        float z = UnityEngine.Random.Range(bounds.min.z, bounds.max.z);
        var position = new Vector3(x, y, z);
        spawnArea.enabled = false;
        return position;
    }
}
