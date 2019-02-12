using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;
public class BasicProjectile : Projectile {

    public UnityEvent OnHit;

    protected Agent.Event.Damage damage;
    protected float speed;
    protected float scale;
    protected float lifespan;
    protected Agent.Type bypassAgentType;

    // references
    private Renderer renderer; // change color
    
    public BasicProjectile() {
        
    }

    protected void OnTriggerEnter(Collider other) {
        Agent agent = other.gameObject.GetComponent<Agent>();
        if (agent)
        {
            if (agent.type != bypassAgentType)
            {
                // Debug.Log("talk shit, get hit");
                agent.ReceiveEvent(damage);
                OnHit.Invoke();
                GameObject.Destroy(this.gameObject);
            }
        }
        else {
            OnHit.Invoke();
            GameObject.Destroy(this.gameObject);
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
            GameObject.Destroy(this.gameObject);
        }
    }

    protected void FixedUpdate() {
        transform.position += transform.forward * speed * Time.fixedDeltaTime;
    }
}
