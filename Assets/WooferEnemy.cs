using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WooferEnemy : BasicAgent
{
    public ParticleSystem deathSystem;

    public WooferEnemy()
    {
    }

    protected override void OnDeath()
    {
        deathSystem.transform.SetParent(null);
        deathSystem.Play();
        deathSystem.Emit(100);
        base.OnDeath();
    }
}
