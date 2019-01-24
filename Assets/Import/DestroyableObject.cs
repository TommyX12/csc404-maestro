using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyableObject : Damageable
{
    public int Hitpoints = 1;
    public override void OnHit(DamageSource damage)
    {
        if((damage.DamageFilter & this.DamageFilter) != 0) {
            Hitpoints -= damage.DamageAmount;
            if (Hitpoints <= 0) {
                Gladiator.player.HitPoints += 5;
                Destroy(gameObject);
            }
        }
    }
}
