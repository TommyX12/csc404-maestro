using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

public class BasicCounterProjectile : CounterProjectile {

    protected const float minimumLineLength = 0.1f;
    protected const float scaleMultiplier = 0.1f;

    public UnityEvent onHit;
    public ParticleGroup hitEffectPrefab;
    protected float scale;
    protected float lifespan;
    protected float totalLifespan;
    protected Projectile counterTarget;

    protected float linePosition;
    protected float lastLinePosition;

    // self reference
    public LineRenderer lineRenderer;

    // references
    private Renderer renderer; // change color
    
    public BasicCounterProjectile() {
        
    }

    private void PlayHitEffect() {
        if (hitEffectPrefab) {
            ParticleGroup pg = ParticleManager.instance.GetParticleGroup(hitEffectPrefab);
            if (pg) {
                pg.transform.position = transform.position;
                pg.PlayOnce();
            }
        }
    }

    protected void OnLifespanReached() {
        if (counterTarget != null && counterTarget.gameObject.activeSelf) {
            onHit.Invoke();
            counterTarget.DestroySelf();
        }
        DestroySelf();
    }

    public override void SetSpawnParameters(SpawnParameters param) {
        if (renderer) {
            renderer.material.color = param.color;
        }
        scale = param.scale;
        totalLifespan = lifespan = param.duration;
        counterTarget = param.counterTarget;
    }

    protected void Awake() {
        if (!lineRenderer) {
            lineRenderer = GetComponent<LineRenderer>();
        }
        
        lineRenderer.enabled = false;
        linePosition = lastLinePosition = 0;
    }

    protected void Start() {
        
    }

    protected void Update() {
        lifespan -= Time.deltaTime;
        
        if (lifespan < 0) {
            OnLifespanReached();
            return;
        }
        
        if (counterTarget == null) {
            DestroySelf();
            return;
        }
        
        lastLinePosition = linePosition;
        linePosition = 1 - (lifespan / totalLifespan);
        lastLinePosition = Mathf.Min(Mathf.Max(lastLinePosition, 0), lastLinePosition - minimumLineLength);
        
        lineRenderer.enabled = true;
        lineRenderer.startWidth = lineRenderer.endWidth = scale * scaleMultiplier;
        lineRenderer.SetPositions(new Vector3[]{
                Vector3.Lerp(host.transform.position, counterTarget.transform.position, lastLinePosition),
                Vector3.Lerp(host.transform.position, counterTarget.transform.position, linePosition)
            });
    }

    protected void FixedUpdate() {
    }

    public override void DestroySelf() {
        PlayHitEffect();
        ProjectileManager.current.KillProjectile(this);
    }
}
