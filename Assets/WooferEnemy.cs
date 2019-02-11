using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WooferEnemy : BasicAgent
{
    public ParticleSystem deathSystem;

    private new void Awake()
    {
        onDeath += DeathAnimation;
        base.Awake();
    }

    void DeathAnimation(Agent agent) {
        deathSystem.transform.SetParent(null);
        deathSystem.Emit(50);
        Destroy(deathSystem.gameObject, 2);
    }

}
