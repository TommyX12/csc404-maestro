using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAgent : BasicAgent
{
    public AudioSource OnDeathClip;

    public PlayerAgent() : base() {
        onDeath += PlayerOnDeath;
    }

    private void PlayerOnDeath(Agent agent)
    {
        this.hitPoint = initialHitPoint;
        transform.position = CheckpointManager.instance.GetActiveCheckpoint().transform.position;
        OnDeathClip.Play();
    }
}
