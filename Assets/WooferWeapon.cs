using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WooferWeapon : BasicWeapon
{
    public ParticleSystem MuzzleFlash;
    public ParticleSystem Charge;

    protected override void OnFire()
    {
        base.OnFire();
        // MuzzleFlash.Play();
        MuzzleFlash.Emit(50);
        // Charge.Play();
        Charge.Emit(50);
    }
}
