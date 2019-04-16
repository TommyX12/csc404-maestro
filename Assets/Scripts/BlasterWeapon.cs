using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlasterWeapon : BasicWeapon
{
    public ParticleSystem MuzzleSystem;
    public ParticleGroup FlashSystem;
    protected override void OnFire()
    {
        base.OnFire();
        MuzzleSystem.Emit(50);
        FlashSystem.PlayOnce();
    }

}
