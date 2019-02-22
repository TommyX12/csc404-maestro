using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WooferWeapon : BasicWeapon
{
    public ParticleSystem MuzzleFlash;
    public ParticleSystem Charge;
    public AudioSource FireSound;
    protected override void OnBeat()
    {
        // MuzzleFlash.Play();
        MuzzleFlash.Emit(50);
        // Charge.Play();
        Charge.Emit(50);
        FireSound.Play();
    }

}
