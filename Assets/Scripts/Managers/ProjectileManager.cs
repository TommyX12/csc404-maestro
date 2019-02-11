using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class ProjectileManager : MonoBehaviour {

    public static ProjectileManager current;
    
    public ProjectileManager() {
        current = this;
    }

    public void SpawnProjectile(Projectile projectilePrefab, Projectile.SpawnParameters param) {
        Projectile projectile = GameObject.Instantiate(projectilePrefab, transform);
        projectile.SetSpawnParameters(param);
    }

    protected void Awake() {
        
    }

    protected void Start() {
        
    }

    protected void Update() {
        
    }
}
