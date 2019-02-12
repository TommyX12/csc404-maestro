using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WooferWeapon : BasicWeapon
{
    public ParticleSystem MuzzleFlash;
    public ParticleSystem Charge;

    public override void Fire()
    {
        base.Fire();
        MuzzleFlash.Emit(50);
        Charge.Emit(50);
    }
}
