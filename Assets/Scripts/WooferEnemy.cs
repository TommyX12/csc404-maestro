using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WooferEnemy : BasicAgent
{
    public ParticleSystem deathSystem;
    public AudioSource deathSound;
    public WooferEnemy()
    {
    }

    protected override void OnDeath()
    {
        deathSystem.transform.SetParent(null);
        deathSystem.Play();
        deathSystem.Emit(100);
        deathSound.Play();
        base.OnDeath();
    }
}
