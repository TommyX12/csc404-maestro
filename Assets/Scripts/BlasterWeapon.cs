using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlasterWeapon : BasicWeapon
{
    public ParticleSystem MuzzleSystem;

    protected override void OnFire()
    {
        base.OnFire();
        MuzzleSystem.Emit(50);
    }

}
