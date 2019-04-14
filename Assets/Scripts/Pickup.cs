using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Assertions;

public class Pickup : MonoBehaviour {

    private PickupManager pickupManager;
    private PickupEffect pickupEffect;

    private float timer = 0;

    public void Initialize(PickupManager pickupManager,
                           GameObject itemParticlePrefab,
                           float duration,
                           PickupEffect pickupEffect) {
        this.pickupManager = pickupManager;
        this.timer = duration;
        this.pickupEffect = pickupEffect;

        SpawnItemParticle(itemParticlePrefab);
    }

    private void SpawnItemParticle(GameObject prefab) {
        var particle = GameObject.Instantiate(prefab, transform);
        particle.transform.localPosition = Vector3.zero;
    }

    private void OnTriggerEnter(Collider other) {
        var gameObject = other.gameObject;
        var playerAgent = gameObject.GetComponent<PlayerAgent>();
        if (playerAgent) {
            playerAgent.AcceptPickup(this);
            pickupManager.StartEffect(pickupEffect);
            DestroySelf();
        }
    }

    private void DestroySelf() {
        GameObject.Destroy(this.gameObject);
        pickupManager.NotifyPickupDestroyed(this);
    }
    
    private void Awake() {
        
    }

    private void Start() {
        
    }

    private void Update() {
        timer -= Time.deltaTime;
        if (timer <= 0) {
            DestroySelf();
        }
    }
}
