using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinnerWeapon : BasicWeapon
{
    public List<GameObject> firePoints;

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
            (projectilePrefab,
             projectileParameters
             .WithTransform(obj.transform.position,
                            obj.transform.forward)
             .WithBypassAgentType(host.type));
        }
        weaponAnim.SetTrigger(triggerIndex);
    }

    private void PlaySound(Riff.NoteHitEvent evt) {
        if (evt.noteIndex != -1) {
            noteSounds[evt.noteIndex].Play();
        }
    }
}
