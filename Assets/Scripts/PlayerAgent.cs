using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAgent : BasicAgent
{
    public AudioSource OnDeathClip;

    protected override void OnDeath()
    {
        PlayerOnDeath(this);
    }

    public override void ReceiveEvent(Event.Damage damage)
    {
        base.ReceiveEvent(damage);

        if (PostProcessEffectManager.current) {
            PostProcessEffectManager.current.PlayHitEffect();
            PostProcessEffectManager.current.UpdateHP(Mathf.RoundToInt(this.hitPoint));
        }

        if (MixerManager.current)
        {
            if (this.hitPoint >= 0)
            {
                MixerManager.current.SetTargetLowpassFreq(MixerManager.current.hpFreqBands[Mathf.RoundToInt(this.hitPoint) - 1]);
            }
        }
    }

    private void PlayerOnDeath(Agent agent)
    {
        this.hitPoint = initialHitPoint; // wait for a bit maybe? coroutine
        if (PostProcessEffectManager.current)
        {
            PostProcessEffectManager.current.UpdateHP(Mathf.RoundToInt(this.hitPoint));
        }
        if (MixerManager.current)
        {
            MixerManager.current.SetTargetLowpassFreq(MixerManager.current.hpFreqBands[Mathf.RoundToInt(this.hitPoint)-1]);
        }
        transform.position = CheckpointManager.instance.GetActiveCheckpoint().transform.position;
        OnDeathClip.Play();
    }
}
