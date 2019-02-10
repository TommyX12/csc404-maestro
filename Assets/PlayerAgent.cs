using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAgent : BasicAgent
{
    public AudioSource OnDeathClip;

    public override void OnDeath()
    {
        this.initialHitPoint = 100;
        transform.position = CheckpointManager.instance.GetActiveCheckpoint().transform.position;
        OnDeathClip.Play();
    }
}
