using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyableObject : Damageable
{
    public int Hitpoints = 1;
    public override void OnHit(int damage, int DamageFilter)
    {
        if((DamageFilter & this.DamageFilter) != 0) {
            Hitpoints -= damage;
            if (Hitpoints <= 0) {
                Destroy(gameObject);
            }
        }
    }
}
