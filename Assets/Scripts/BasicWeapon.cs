using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class BasicWeapon : Weapon {
    
    public const int beatsPerCycle = 4;
    
    protected Riff riff;
    protected Agent host;

    // references
    public Renderer renderer; // change color

    // exposed parameters
    public List<Riff.Note> notes = new List<Riff.Note>{new Riff.Note(0)};
    public string defaultSound = "kick-1";
    public Color color = new Color(0.2f, 0.4f, 0.8f);
    public bool noisyFollow = false;
    public Projectile projectilePrefab = null;
    public Projectile.SpawnParameters projectileParameters = new Projectile.SpawnParameters {
        damage = new Agent.Event.Damage {amount = 10.0f},
        color = new Color(0.2f, 0.4f, 0.8f),
        speed = 10.0f,
        scale = 1.0f,
        distance = 100.0f
    };

    protected bool autoFire = false;
    
    public BasicWeapon() {
        
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

    protected void DelayedNoteHitEventHandler(Riff.NoteHitEvent e) {
        if (e.noteIndex != -1 && autoFire == e.automatic) {
            OnFire();
        }
    }

    protected void NoteHitEventHandler(Riff.NoteHitEvent e) {
        if (e.automatic) {
            OnBeat();
        }
    }

    protected virtual void OnBeat() {
        // MusicManager.Current.PlayOnce("kick-1");
    }

    protected virtual void OnFire() {
        ProjectileManager.current.SpawnProjectile 
            (projectilePrefab,
             projectileParameters
             .WithTransform(transform.position,
                            transform.forward)
             .WithBypassAgentType(host.type));
    }

    public override Riff GetRiff() {
        return riff;
    }

    public override void Fire() {
        riff.ButtonPress();
    }

    protected void Awake() {
        riff = new Riff(beatsPerCycle, notes, MusicManager.Current);
        riff.delayedNoteHitEvent += DelayedNoteHitEventHandler;
        riff.noteHitEvent += NoteHitEventHandler;
        riff.defaultSound = defaultSound;
        riff.playing = false;

        if (renderer) {
            renderer.material.color = color;
        }
    }

    protected void Start() {
        
    }

    protected void FixedUpdate() {
        riff.Update();
    }
}
