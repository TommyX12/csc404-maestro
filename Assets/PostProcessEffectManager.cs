using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
public class PostProcessEffectManager : MonoBehaviour
{

    public static PostProcessEffectManager current = null;

    public PostProcessVolume volume;

    private Grayscale grayscaleEffect;
    private Pixelize pixelizeEffect;

    private float hitEffectTimer = 1;
    public AnimationCurve pixelizeHitEffectCurve;
    public AnimationCurve grayscaleHPEffectCurve;


    private float hpEffectIndex = 8;
    private float hpEffectTargetIndex = 8;

    private void Awake()
    {
        if (!volume) {
            Destroy(this);
            return;
        }
        volume.profile.TryGetSettings(out grayscaleEffect);
        volume.profile.TryGetSettings(out pixelizeEffect);
        current = this;
    }

    public void PlayHitEffect() {
        hitEffectTimer = 0;
    }

    public void UpdateHP(int hp) {
        hpEffectTargetIndex = hp;
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
    }

    private void Update()
    {
        UpdateHPEffect();
        UpdateHitEffect();
    }

}
