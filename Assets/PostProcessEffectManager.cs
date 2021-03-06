﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
public class PostProcessEffectManager : MonoBehaviour
{

    public static PostProcessEffectManager current = null;

    public PostProcessVolume volume;

    private GrayEffect grayscaleEffect;
    private Pixelize pixelizeEffect;
    private RedEffect redEffect;

    private float hitEffectTimer = 1;
    public AnimationCurve pixelizeHitEffectCurve;
    public AnimationCurve grayscaleHPEffectCurve;
    public AnimationCurve redFlashEffectCurve;

    private float hpEffectIndex = 8;
    private float hpEffectTargetIndex = 8;

    private void Awake()
    {
        if (!volume) {
            Destroy(this);
            return;
        }
        volume.profile.TryGetSettings(out grayscaleEffect);
        if (grayscaleEffect == null)
        {
            volume.profile.AddSettings(typeof(GrayEffect));
            volume.profile.TryGetSettings(out grayscaleEffect);
            grayscaleEffect.SetAllOverridesTo(true);
            grayscaleEffect.blend.value = 0;
        }

        volume.profile.TryGetSettings(out pixelizeEffect);
        volume.profile.TryGetSettings(out redEffect);

        current = this;
    }

    public void PlayHitEffect() {
        hitEffectTimer = 0;
    }

    public void UpdateHP(BasicAgent agent) {
        hpEffectTargetIndex = agent.hitPoint / agent.initialHitPoint;
    }

    public void UpdateHPEffect() {
        hpEffectIndex = Mathf.Lerp(hpEffectIndex, hpEffectTargetIndex, Time.deltaTime);
        grayscaleEffect.blend.value = grayscaleHPEffectCurve.Evaluate(hpEffectIndex);
    }

    public void UpdateHitEffect() {
        if (hitEffectTimer < 1)
        {
            hitEffectTimer += Time.deltaTime;
        }
        else
        {
            hitEffectTimer = 1;
        }
        pixelizeEffect.pixelSize.value = Mathf.RoundToInt(pixelizeHitEffectCurve.Evaluate(hitEffectTimer));
        redEffect.blend.value = redFlashEffectCurve.Evaluate(hitEffectTimer);
    }

    private void Update()
    {
        UpdateHPEffect();
        UpdateHitEffect();
    }

}
