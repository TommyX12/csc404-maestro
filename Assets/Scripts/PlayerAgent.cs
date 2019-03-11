using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAgent : BasicAgent
{
    public AudioSource OnDeathClip;

    public float healthRegenDelay = 5;
    private float healthRegenDelayTimer = 0;
    public float healthRegenRate = 1;
    public float invulnerabilityTime = 1f;
    public float invulnerabilityTimer = 0f;

    protected override void OnDeath()
    {
        PlayerOnDeath(this);
    }

    public override void ReceiveEvent(Event.Damage damage)
    {
        if (invulnerabilityTimer >= 0) {
            return;
        }

        base.ReceiveEvent(damage);

        healthRegenDelayTimer = 5;

        if (PostProcessEffectManager.current) {
            PostProcessEffectManager.current.PlayHitEffect();
            PostProcessEffectManager.current.UpdateHP(Mathf.RoundToInt(this.hitPoint));
        }

        if (MixerManager.current)
        {
            if (this.hitPoint >= 0)
            {
                MixerManager.current.SetTargetLowpassFreq(MixerManager.current.hpFreqBands[Mathf.CeilToInt(this.hitPoint) - 1]);
            }
        }
        model.PlayerHit();
        invulnerabilityTimer = invulnerabilityTime;
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
        model.PlayerDied();
    }

    private void UpdateScreenEffects() {
        if (PostProcessEffectManager.current)
        {
            PostProcessEffectManager.current.UpdateHP(Mathf.RoundToInt(this.hitPoint));
        }
        if (MixerManager.current)
        {
            MixerManager.current.SetTargetLowpassFreq(MixerManager.current.hpFreqBands[Mathf.RoundToInt(this.hitPoint) - 1]);
        }
    }

    private new void Update()
    {
        base.Update();
        healthRegenDelayTimer -= Time.deltaTime;
        invulnerabilityTimer -= Time.deltaTime;

        if (healthRegenDelayTimer < 0) {
            healthRegenDelayTimer = 0;
            hitPoint += healthRegenRate * Time.deltaTime;
            hitPoint = Mathf.Min(hitPoint, initialHitPoint);
            UpdateScreenEffects();
        }
    }
}
