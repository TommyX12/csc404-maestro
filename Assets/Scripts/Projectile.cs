using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public abstract class Projectile : MonoBehaviour, ObjectPoolable<Projectile> {

    private int poolID;

    protected Agent host;

    public bool usePool = true;

    [Serializable]
    public struct SpawnParameters {
        public Vector3 position;
        public Vector3 direction;
        public Agent.Event.Damage damage;
        public Color color;
        public float speed;
        public float scale;
        public float distance;
        public Agent.Type bypassAgentType;
        public float duration;
        public Projectile counterTarget;

        public SpawnParameters WithTransform(Vector3 position, Vector3 direction) {
            this.position = position;
            this.direction = direction;
            return this;
        }

        public SpawnParameters WithBypassAgentType(Agent.Type type) {
            this.bypassAgentType = type;
            return this;
        }

        public SpawnParameters WithCounterTarget(Projectile counterTarget) {
            this.counterTarget = counterTarget;
            return this;
        }
    }

    public void SetPoolID(int id) {
        this.poolID = id;
    }

    public int GetPoolID() {
        return poolID;
    }

    public void SetHost(Agent host) {
        this.host = host;
    }

    public Agent GetHost() {
        return host;
    }

    public Projectile() {
        
    }

    public abstract void SetSpawnParameters(SpawnParameters param);

    public Projectile CreateNew() {
        Projectile projectile = GameObject.Instantiate(this);
        projectile.gameObject.SetActive(false);
        projectile.name = this.name;
        projectile.transform.SetParent(ProjectileManager.current.transform);
        return projectile;
    }

    public virtual void DestroySelf() {
        ProjectileManager.current.KillProjectile(this);
    }

}
