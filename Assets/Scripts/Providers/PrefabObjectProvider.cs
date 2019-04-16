using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Assertions;

public class PrefabObjectProvider : MonoBehaviour {

    [SerializeField]
    private Pickup pickupPrefab;

    [SerializeField]
    private GameObject doubleShotStartParticle;
    [SerializeField]
    private GameObject speedBoostStartParticle;
    [SerializeField]
    private GameObject shieldStartParticle;
    [SerializeField]
    private GameObject extraBeatsStartParticle;

    [SerializeField]
    private GameObject doubleShotItemParticle;
    [SerializeField]
    private GameObject speedBoostItemParticle;
    [SerializeField]
    private GameObject shieldItemParticle;
    [SerializeField]
    private GameObject extraBeatsItemParticle;

    [SerializeField]
    private GuideLine guideLinePrefab;

    private Dictionary<PickupEffect.PickupEffectType, GameObject> startEffectParticle;
    private Dictionary<PickupEffect.PickupEffectType, GameObject> itemEffectParticle;

    private void Awake() {
        Assert.IsNotNull(pickupPrefab, "pickupPrefab");
        Assert.IsNotNull(doubleShotStartParticle, "doubleShotStartParticle");
        Assert.IsNotNull(speedBoostStartParticle, "speedBoostStartParticle");
        Assert.IsNotNull(shieldStartParticle, "shieldStartParticle");
        Assert.IsNotNull(extraBeatsStartParticle, "extraBeatsStartParticle");
        Assert.IsNotNull(doubleShotItemParticle, "doubleShotItemParticle");
        Assert.IsNotNull(speedBoostItemParticle, "speedBoostItemParticle");
        Assert.IsNotNull(shieldItemParticle, "shieldItemParticle");
        Assert.IsNotNull(extraBeatsItemParticle, "extraBeatsItemParticle");
        Assert.IsNotNull(guideLinePrefab, "guideLinePrefab");

        startEffectParticle = new Dictionary<PickupEffect.PickupEffectType, GameObject>() {
            {PickupEffect.PickupEffectType.DOUBLE_SHOT, doubleShotStartParticle},
            {PickupEffect.PickupEffectType.SPEED_BOOST, speedBoostStartParticle},
            {PickupEffect.PickupEffectType.SHIELD, shieldStartParticle},
            {PickupEffect.PickupEffectType.EXTRA_BEATS, extraBeatsStartParticle}
        };

        itemEffectParticle = new Dictionary<PickupEffect.PickupEffectType, GameObject>() {
            {PickupEffect.PickupEffectType.DOUBLE_SHOT, doubleShotItemParticle},
            {PickupEffect.PickupEffectType.SPEED_BOOST, speedBoostItemParticle},
            {PickupEffect.PickupEffectType.SHIELD, shieldItemParticle},
            {PickupEffect.PickupEffectType.EXTRA_BEATS, extraBeatsItemParticle}
        };
    }

    private void Start() {
        
    }

    private void Update() {
        
    }

    public GuideLine GetGuideLine() {
        return guideLinePrefab;
    }

    public Pickup GetPickup() {
        return pickupPrefab;
    }

    public GameObject GetPickupItemParticle(PickupEffect.PickupEffectType type) {
        GameObject result = null;
        itemEffectParticle.TryGetValue(type, out result);
        return result;
    }

    public GameObject GetPickupStartParticle(PickupEffect.PickupEffectType type) {
        GameObject result = null;
        startEffectParticle.TryGetValue(type, out result);
        return result;
    }
}
