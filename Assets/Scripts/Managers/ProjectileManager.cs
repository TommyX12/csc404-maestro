using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class ProjectileManager : MonoBehaviour {

    public static ProjectileManager current;

    private Dictionary<string, ObjectPool<Projectile>> pools = new Dictionary<string, ObjectPool<Projectile>>();

    public ProjectileManager() {
        current = this;
    }

    public void SpawnProjectile(Projectile projectilePrefab, Projectile.SpawnParameters param) {
        if (!pools.ContainsKey(projectilePrefab.name))
        {
            pools.Add(projectilePrefab.name, new ObjectPool<Projectile>(projectilePrefab.CreateNew, 1, ReleaseDelegate, RequestDelegate)); // 1 for testing
        }
        Projectile projectile = pools[projectilePrefab.name].Request();
        projectile.SetSpawnParameters(param);
    }

    public void KillProjectile(Projectile proj) {
        if (!pools.ContainsKey(proj.name))
        {
            Debug.LogError("No live projectile for projectile: " + proj.name);
            return;
        }
        pools[proj.name].Release(proj);
    }

    public void RequestDelegate(Projectile proj) {
        proj.gameObject.SetActive(true);
    }

    public void ReleaseDelegate(Projectile proj)
    {
        proj.gameObject.SetActive(false);
    }

    protected void Awake() {
        
    }

    protected void Start() {
        
    }

    protected void Update() {
        
    }
}
