﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// testing
// currently this shotgun only raycasts
// Think of it as firing a "slug"
public class Shotgun : MonoBehaviour
{
    public AudioSource FireSound;
    public AudioSource FireSoundReload1;
    public AudioSource FireSoundReload2;
    public AudioSource JammedSound;

    public ParticleSystem psystem;

    public GameObject FirePoint;

    public float tol = 0.05f;

    public bool Jammed = false;

    private GameObject target = null;

    public List<Riff.Note> notes;
    public int BeatsPerBar;
    public bool PreviewRhythm = false;
    protected Riff riff;
    
    protected void Init() {
        riff = new Riff(BeatsPerBar, notes, MusicManager.current);
    }

    private void Start()
    {
        Init();
        riff.noteHitEvent += noteHitEventHandler;
    }

    public void StopAllSounds() {
        FireSound.Stop();
        FireSoundReload1.Stop();
        FireSoundReload2.Stop();
        JammedSound.Stop();
    }

    public void Fire()
    {
        Riff.NoteHitEvent press = riff.ButtonPress();
        // if (Jammed) {
        //     return;
        // }
        if (press.noteIndex != -1)
        {
            switch (press.noteIndex) {
                case 0:
                    psystem.Emit(25);
                    MusicManager.current.PlayAudioSourceAligned(FireSoundReload1, 4);
                    RaycastAndDamage();
                    break;
                case 1:
                    psystem.Emit(25);
                    MusicManager.current.PlayAudioSourceAligned(FireSound, 4);
                    RaycastAndDamage();
                    break;
                case 2:
                    psystem.Emit(25);
                    MusicManager.current.PlayAudioSourceAligned(FireSoundReload2, 4);
                    RaycastAndDamage();
                    break;
                default:
                    Jammed = true;
                    StopAllSounds();
                    JammedSound.Play();
                    break;
            }
        } else {
            Jammed = true;
            StopAllSounds();
            JammedSound.Play();
        }
    }

    public void RaycastAndDamage() {
        RaycastHit hit;
        Physics.Raycast(FirePoint.transform.position, FirePoint.transform.forward, out hit, 10f);
        if (hit.collider && hit.collider.gameObject.GetComponent<Damageable>()) {
            hit.collider.gameObject.GetComponent<Damageable>().OnHit(10, 1); // hardcoded damage value for shotgun right now.
        }
    }

    private void noteHitEventHandler(Riff.NoteHitEvent e) {
        if (PreviewRhythm) {
            if (e.automatic) {
                StaticAudioManager.current.GetPreviewSound().Play();
            }
        }
    }

    public void FixedUpdate()
    {
        this.riff.Update();

        if (Jammed) {
            if (!JammedSound.isPlaying) {
                Jammed = false;
            }
        }

        if (target == null) {
            target = EnemyManagementSystem.current.GetNextEnemy();
        }

        if (target != null)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(target.transform.position - transform.position, Vector3.up), Time.fixedDeltaTime * 100);
        };

    }
}
