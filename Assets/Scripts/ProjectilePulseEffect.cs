using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilePulseEffect : MonoBehaviour
{
    public ParticleSystem psys;
    public float ActiveTime = 1f;
    private float ActiveTimer = 0;

    public void OnBeat() {
        ActiveTimer = ActiveTime;
        psys.Play();
    }

    private void Update()
    {
        ActiveTimer -= Time.deltaTime;
        if (ActiveTimer < 0) {
            psys.Stop();
        }
    }

}
