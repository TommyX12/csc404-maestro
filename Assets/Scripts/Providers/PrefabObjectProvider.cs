using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Assertions;

public class PrefabObjectProvider : MonoBehaviour {

    [SerializeField]
    private Pickup pickupPrefab;

    private void Awake() {
        Assert.IsNotNull(pickupPrefab);
    }

    private void Start() {
        
    }

    private void Update() {
        
    }

    public Pickup GetPickup() {
        return pickupPrefab;
    }
}
