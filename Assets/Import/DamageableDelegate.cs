using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageableDelegate : Damageable
{

    public Damageable Delegate;

    private void Start()
    {
        this.DamageFilter = Delegate.DamageFilter;
    }

    public override void OnHit(DamageSource damage)
    {
        Delegate.OnHit(damage);
    }
}
