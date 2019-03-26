using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
public class MixerManager : MonoBehaviour
{
    public static MixerManager current;
    public AudioMixerGroup master;
    public AudioMixerGroup playerHitMixer;

    public AnimationCurve playerHitLowpassCurve;

    public float interpSpeed = 1;

    public List<float> hpFreqBands;

    [Range(0,22000)]
    public float targetLowPass = 22000.00f;
    public float targetHighPass = 0;

    private float interpVal = 0;

    public void PlayerGotHit()
    {
        interpVal = 1;
    }

    private void Awake()
    {
        current = this;
    }

    public void SetTargetLowpassFreq(float targetLowPass) {
        this.targetLowPass = targetLowPass;
    }

    private void Update()
    {
        float val;
        master.audioMixer.GetFloat("LowpassFreq", out val);
        master.audioMixer.SetFloat("LowpassFreq", Mathf.Lerp(val, targetLowPass, Time.deltaTime*interpSpeed));

        interpVal -= Time.deltaTime;
        interpVal = interpVal < 0 ? 0 : interpVal;

        master.audioMixer.SetFloat("LowpassFreq", playerHitLowpassCurve.Evaluate(interpVal));
    }
}
