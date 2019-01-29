using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// testing
 // currently this shotgun only raycasts
 // Think of it as firing a "slug"
public class Shotgun : Weapon
{
    // audio sounds for this gun
    public AudioSource FireSound;
    public AudioSource FireSoundReload1;
    public AudioSource FireSoundReload2;
    public AudioSource JammedSound;

    // particle system for shotgun projectiles
    public ParticleSystem psystem;

    // The firing point of the shotgun
    public GameObject FirePoint;

    // Tolerance for auto-input
    public float tol = 0.05f;

    public bool Jammed = false;

    // Weapon Target
    private GameObject target = null;

    private void Start()
    {
        Init();
        riff.hitOffset = 0; // Set hit-offset to zero, otherwise input is wierd
    }

    public void StopAllSounds() {
        FireSound.Stop();
        FireSoundReload1.Stop();
        FireSoundReload2.Stop();
        JammedSound.Stop();
    }

    public override void Fire()
    {
        if (Jammed) {
            return;
        }

        Riff.ButtonPressResult press = riff.ButtonPress();

        // check if there was a hit and process accordingly
        if (press.noteIndex != -1)
        {
            switch (press.noteIndex) {
                case 0:
                    psystem.Emit(25);
                    FireSoundReload1.Play();
                    RaycastAndDamage();
                    break;
                case 1:
                    psystem.Emit(25);
                    FireSound.Play();
                    RaycastAndDamage();
                    break;
                case 2:
                    psystem.Emit(25);
                    FireSoundReload2.Play();
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

    public void FixedUpdate()
    {
        // update the riff
        this.riff.Update();

        // Jam until the jammed sound is done
        if (Jammed) {
            if (!JammedSound.isPlaying) {
                Jammed = false;
            }
        }


        if (PreviewRhythm)
        {
            riff.hitOffset = 0.2f;
            Riff.ButtonPressResult press = riff.ButtonPress();
            if (press.noteIndex != -1 && press.deltaTime < tol)
            {
                StaticAudioManager.current.GetPreviewSound().Play();
            }
        }
        else {
            riff.hitOffset = 0;
        }

        if (target == null) {
            target = EnemyManagementSystem.current.GetNextEnemy();
        }

        if (target != null)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(target.transform.position - transform.position, Vector3.up), Time.fixedDeltaTime * 100);
        };

    }

    public override void BeginPreview()
    {
        base.BeginPreview();
        riff.hitOffset = 0.2f; // otherwise the autoplay is wierd
    }

    public override void StopPreview()
    {
        base.StopPreview();
        riff.hitOffset = 0f; // otherwise the autoplay is wierd
    }
}
