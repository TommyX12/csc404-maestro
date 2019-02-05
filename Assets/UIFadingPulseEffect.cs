using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFadingPulseEffect : MonoBehaviour
{
    public float PulseDuration = 1f;
    public Color RestColor;
    public Color PulseColor;
    public Image target;

    private float pulseTimer;

    private float interpolation() {
       return pulseTimer / PulseDuration;
    }

    public void Pulse() {
        target.color = PulseColor;
        pulseTimer = PulseDuration;
    }

    public void Update()
    {
        target.color = RestColor + (PulseColor - RestColor) * interpolation();
        if (pulseTimer > 0) {
            pulseTimer -= Time.deltaTime;
        }
    }

}
