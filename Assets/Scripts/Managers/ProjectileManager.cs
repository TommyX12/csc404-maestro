using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class ProjectileManager : MonoBehaviour {

    public static ProjectileManager current;

    private Dictionary<string, ObjectPool<Projectile>> pools = new Dictionary<string, ObjectPool<Projectile>>();

    private Dictionary<int, List<Projectile>> hostToProjectiles = new Dictionary<int, List<Projectile>>();

    public ProjectileManager() {
        current = this;
    }

    public void SpawnProjectile(Agent host, Projectile projectilePrefab, Projectile.SpawnParameters param) {
        if (!pools.ContainsKey(projectilePrefab.name))
        {
            pools.Add(projectilePrefab.name, new ObjectPool<Projectile>(projectilePrefab.CreateNew, 100, ReleaseDelegate, RequestDelegate));
        }
        Projectile projectile = pools[projectilePrefab.name].Request();
        projectile.SetHost(host);
        projectile.SetSpawnParameters(param);

        // track projectile host
        if (host) {
            int hostUID = host.GetUID();
            if (!hostToProjectiles.ContainsKey(hostUID)) {
                hostToProjectiles.Add(hostUID, new List<Projectile>());
            }
            hostToProjectiles[hostUID].Add(projectile);
        }
    }

    /// <summary>
    ///   It seems important that this method is called when a projectile dies.
    /// </summary>
    public void KillProjectile(Projectile proj) {
        if (!pools.ContainsKey(proj.name))
        {
            Debug.LogError("No live projectile for projectile: " + proj.name);
            return;
        }
        pools[proj.name].Release(proj);

        // track projectile host
        Agent host = proj.GetHost();
        if (host) {
            int hostUID = host.GetUID();
            hostToProjectiles[hostUID].Remove(proj);
            if (hostToProjectiles[hostUID].Count == 0) {
                hostToProjectiles.Remove(hostUID);
            }
        }
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

    public Projectile GetClosestProjectileOf(Agent agent) {
        if (!agent) return null;

        Vector3 agentPosition = agent.transform.position;
        int uid = agent.GetUID();
        if (!hostToProjectiles.ContainsKey(uid)) {
            return null;
        }
        var projectiles = hostToProjectiles[uid];
        float bestDistance = Mathf.Infinity;
        Projectile result = null;
        foreach (var projectile in projectiles) {
            Vector3 position = projectile.transform.position;
            float distance = (position - agentPosition).sqrMagnitude;
            if (distance < bestDistance) {
                bestDistance = distance;
                result = projectile;
            }
        }

        return result;
    }
}
