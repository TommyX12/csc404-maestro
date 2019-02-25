using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class BasicCountermeasure : Countermeasure {
    
    public const int beatsPerCycle = 4;
    
    protected Agent host;
    protected bool autoFire = false;
    protected Agent target;
    protected Riff targetRiff;

    // references
    public Renderer renderer; // change color

    // exposed parameters
    public Color color = new Color(0.2f, 0.4f, 0.8f);
    public bool noisyFollow = false;
    public CounterProjectile projectilePrefab = null;
    public Projectile.SpawnParameters projectileParameters = new Projectile.SpawnParameters {
        scale = 1.0f,
        duration = 0.1f
    };

    public BasicCountermeasure() {
        
    }

    public override void SetTarget(Agent target) {
        this.target = target;
    }

    public override void SetHost(Agent agent) {
        host = agent;
        
        if (host) {
            if (!GetComponent<NoisyFollow>() && noisyFollow) {
                NoisyFollow follow = gameObject.AddComponent<NoisyFollow>();
                follow.target = host.transform;
            }
        }
        else {
            GetComponent<NoisyFollow>().target = null;
        }
    }

    public void SetAutoFire(bool autoFire) {
        this.autoFire = autoFire;
    }

    protected void SubscribeRiff(Riff riff) {
        riff.delayedNoteHitEvent += DelayedNoteHitEventHandler;
        riff.noteHitEvent += NoteHitEventHandler;
    }

    protected void UnsubscribeRiff(Riff riff) {
        riff.delayedNoteHitEvent -= DelayedNoteHitEventHandler;
        riff.noteHitEvent -= NoteHitEventHandler;
    }

    protected void DelayedNoteHitEventHandler(Riff.NoteHitEvent e) {
        if (e.noteIndex != -1 && autoFire == e.automatic) {
            OnFire();
        }
    }

    protected void NoteHitEventHandler(Riff.NoteHitEvent e) {
        if (autoFire == e.automatic) {
            OnBeat();
        }
    }

    protected virtual void OnBeat() {
        // MusicManager.Current.PlayOnce("kick-1");
    }

    protected virtual void OnFire() {
        if (target) {
            Projectile projectile = ProjectileManager.current.GetClosestProjectileOf(target);
            if (projectile != null) {
                ProjectileManager.current.SpawnProjectile 
                    (host,
                     projectilePrefab,
                     projectileParameters
                     .WithCounterTarget(projectile),
                     /* no pooling */ true);
            }
        }
    }

    public override Riff GetTargetRiff() {
        return targetRiff;
    }

    protected void UpdateTargetRiff() {
        Riff riff;
        if (target) {
            riff = target.GetRiff();
        }
        else {
            riff = null;
        }
        
        if (riff != targetRiff) {
            if (targetRiff != null) {
                UnsubscribeRiff(targetRiff);
            }
            if (riff != null) {
                SubscribeRiff(riff);
            }
            targetRiff = riff;
        }
    }

    public override void Fire() {
        Riff riff = GetTargetRiff();
        if (riff != null) {
            riff.ButtonPress();
        }
    }

    protected void Awake() {
        if (renderer) {
            renderer.material.color = color;
        }
    }

    protected void Start() {
        
    }

    protected void Update() {
        UpdateTargetRiff();
    }
}
