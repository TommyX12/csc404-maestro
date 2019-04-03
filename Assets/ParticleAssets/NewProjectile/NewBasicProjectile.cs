using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;
public class NewBasicProjectile : Projectile {

    public UnityEvent onHit;
    public ParticleSystem hitEffectPrefab;
    public ParticleSystem bulletTrailPS;
    public TrailRenderer bulletTrail;
    //public ParticleGroup hitEffectPrefab;
    protected Agent.Event.Damage damage;
    protected float speed;
    protected float scale;
    protected float lifespan;
    protected Agent.Type bypassAgentType;
    private ParticleSystem trailPSPos;
    private TrailRenderer trailPos;


    // references
    private Renderer renderer; // change color
    
    public NewBasicProjectile() {
        
    }

    private void PlayHitEffect() {
        if (hitEffectPrefab)
        {
        	var hitVFX = Instantiate (hitEffectPrefab, transform.position,  transform.rotation);
        	/*var psHit = hitEffect.GetComponent<ParticleSystem>();
        	if(psHit != null)
        	{
        		if(hitEffect != null){
        			Destroy(hitEffect, psHit.main.duration);
        		}
        	}
        	else
        	{
        		var psChild = hitEffect.transform.GetChild(0).GetComponent<ParticleSystem>();
        		if(hitEffect != null){
        			Destroy(hitEffect, psChild.main.duration);
        		}
        	}*/
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
                DestroySelf();
            }
        }
        else {
            onHit.Invoke();
            DestroySelf();
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
        /*if(bulletTrail){
            trailPos = Instantiate (bulletTrail, transform.position,  transform.rotation);
        }*/
        if(bulletTrailPS)
        {
            trailPSPos = Instantiate (bulletTrailPS, transform.position,  transform.rotation);
        }
    }

    protected void Awake() {

    }

    protected void Start() {

    }

    protected void Update() {
        lifespan -= Time.deltaTime;
        if (lifespan < 0) {
            DestroySelf();
        }
    }

    protected void FixedUpdate() {
        transform.position += transform.forward * speed * Time.fixedDeltaTime;
        /*if(bulletTrail){
            trailPos.transform.position = transform.position;
        }*/
        if(bulletTrailPS)
        {
            trailPSPos.transform.position = transform.position;
        }
    }

    public override void DestroySelf() {
        PlayHitEffect();
        if(trailPSPos)
        {
        	var emission = trailPSPos.emission;
        	emission.rateOverTime = 0F;
        }
        if(trailPos)
        {
            trailPos.emitting = false;
        }
        /*(if (bulletTrail && trailPos != null)
        {
        	var psHit = trailPos.GetComponent<ParticleSystem>();
        	if(psHit != null)
        	{
        		var emission = psHit.emission;
        		emission.rateOverTime = 0F;
        		Destroy(trailPos, psHit.main.duration);
        	}
        	else
        	{
        		var psChild = trailPos.transform.GetChild(0).GetComponent<ParticleSystem>();
        		var emission = psChild.emission;
        		emission.rateOverTime = 0F;
        		Destroy(trailPos, psChild.main.duration);
        	}
        }*/
        ProjectileManager.current.KillProjectile(this);
    }
}
