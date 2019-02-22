using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;
public class BasicProjectile : Projectile {

    public UnityEvent onHit;
    public ParticleGroup hitEffectPrefab;
    protected Agent.Event.Damage damage;
    protected float speed;
    protected float scale;
    protected float lifespan;
    protected Agent.Type bypassAgentType;

    // references
    private Renderer renderer; // change color
    
    public BasicProjectile() {
        
    }

    private void PlayHitEffect() {
        if (hitEffectPrefab)
        {
            ParticleGroup pg = ParticleManager.instance.GetParticleGroup(hitEffectPrefab);
            if (pg)
            {
                pg.transform.position = transform.position;
                pg.PlayOnce();
            }
        }
    }

    protected void OnTriggerEnter(Collider other) {
        Agent agent = other.gameObject.GetComponent<Agent>();
        if (agent)
        {
            if (agent.type != bypassAgentType)
            {
                // Debug.Log("talk shit, get hit");
                agent.ReceiveEvent(damage.WithForceDirection(transform.forward));
                onHit.Invoke();
                PlayHitEffect();
                ProjectileManager.current.KillProjectile(this);
            }
        }
        else {
            onHit.Invoke();
            PlayHitEffect();
            ProjectileManager.current.KillProjectile(this);
        }
    }

    public override void SetSpawnParameters(SpawnParameters param) {
        transform.position = param.position;
        transform.forward = param.direction.normalized;
        damage = param.damage;
        if (renderer) {
            renderer.material.color = param.color;
        }
        speed = param.speed;
        scale = param.scale;
        lifespan = param.distance / speed;
        bypassAgentType = param.bypassAgentType;
    }

    protected void Awake() {
        
    }

    protected void Start() {
        
    }

    protected void Update() {
        lifespan -= Time.deltaTime;
        if (lifespan < 0) {
            PlayHitEffect();
            ProjectileManager.current.KillProjectile(this);
        }
    }

    protected void FixedUpdate() {
        transform.position += transform.forward * speed * Time.fixedDeltaTime;
    }
}
