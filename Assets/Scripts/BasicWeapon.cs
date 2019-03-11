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

    protected List<AudioSource> noteSounds = new List<AudioSource> ();

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
        if (autoFire == e.automatic) {
            OnBeat();
        }
    }

    protected virtual void OnBeat() {
        // MusicManager.Current.PlayOnce("kick-1");
    }

    protected virtual void OnFire() {
        ProjectileManager.current.SpawnProjectile 
            (host,
             projectilePrefab,
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
        riff = new Riff(beatsPerCycle, notes, MusicManager.current);
        riff.delayedNoteHitEvent += DelayedNoteHitEventHandler;
        riff.noteHitEvent += NoteHitEventHandler;
        riff.defaultSound = defaultSound;
        riff.playing = false;

        if (renderer) {
            renderer.material.color = color;
        }



        // create audio listeners for reach note

        AudioSource templateSource = GetComponent<AudioSource>();

        List<string> uniqueAudioSounds = new List<string>();
        foreach (Riff.Note note in riff.GetNotes()) {
            if (!uniqueAudioSounds.Contains(note.sound)) {
                uniqueAudioSounds.Add(note.sound);
            }
        }

        AudioClip defaultClip = ResourceManager.GetMusic(riff.defaultSound);

        if (!defaultClip) {
            Debug.LogError("No default clip " + defaultClip);
        }

        AudioSource defaultSource = gameObject.AddComponent<AudioSource>();
        if (templateSource)
        {
            Util.CopyAudioSource(templateSource, defaultSource);
        }
        defaultSource.clip = defaultClip;
        defaultSource.outputAudioMixerGroup = MusicManager.current.Mixer; //  temp set do better @TODO;

        Dictionary<string, AudioSource> soundMapping = new Dictionary<string, AudioSource>();

        foreach (string audioSound in uniqueAudioSounds) {
            AudioClip clip = ResourceManager.GetMusic(audioSound);
            
            if (clip) {
                AudioSource source = gameObject.AddComponent<AudioSource>();
                if (templateSource)
                {
                    Util.CopyAudioSource(templateSource, source);
                }
                source.clip = clip;
                source.outputAudioMixerGroup = MusicManager.current.Mixer; // temp set. do better later
                // @todo set a mixer
                soundMapping[audioSound] = source;
            }

        }
        foreach (Riff.Note note in riff.GetNotes()) {
            if (soundMapping.ContainsKey(note.sound))
            {
                this.noteSounds.Add(soundMapping[note.sound]);
            }
            else {
                this.noteSounds.Add(defaultSource);
            }
        }
    }

    protected void Start() {
        
    }

    protected void FixedUpdate() {
        riff.Update();
    }
}
