using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiWeapon : BasicWeapon
{
    public List<GameObject> firePoints;
    public List<ParticleGroup> particleGroups = new List<ParticleGroup>();
    private Animator weaponAnim;

    private int triggerIndex = Animator.StringToHash("Fire");

    private new void Start()
    {
        List<Riff.Note> notes = this.riff.GetNotes();
        base.Start();
        weaponAnim = GetComponent<Animator>();
        riff.noteHitEvent += PlaySound;
    }

    protected override void OnFire()
    {
        foreach (GameObject obj in firePoints) {
                    ProjectileManager.current.SpawnProjectile 
                        (host,
                         projectilePrefab,
             projectileParameters
             .WithTransform(obj.transform.position,
                            obj.transform.forward)
             .WithBypassAgentType(host.type));
        }

        foreach (ParticleGroup pg in particleGroups) {
            pg.PlayOnce();
        }

        if (weaponAnim)
        {
            weaponAnim.SetTrigger(triggerIndex);
        }
    }

    private void PlaySound(Riff.NoteHitEvent evt) {
        if (autoFire && evt.noteIndex != -1) {
            noteSounds[evt.noteIndex].Play();
        }
    }
}
