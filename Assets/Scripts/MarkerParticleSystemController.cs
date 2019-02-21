using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class MarkerParticleSystemController : MonoBehaviour
{
    private ParticleSystem psys;
    private ParticleSystemRenderer psr;
    public GameObject Marker;
    private void Start()
    {
        psys = GetComponent<ParticleSystem>();
        psr = GetComponent<ParticleSystemRenderer>();
    }

    private void Update()
    {
        float rot = transform.localEulerAngles.y;
        psr.alignment = ParticleSystemRenderSpace.Local;
        float delta = (Marker.transform.position - transform.position).magnitude;
        var pmain = psys.main;
        pmain.startLifetime = delta / psys.main.startSpeed.constant;
        ParticleSystem.Particle[] particles = new ParticleSystem.Particle[psys.main.maxParticles];
        int pcount = psys.GetParticles(particles);
        for (int i = 0; i < pcount; i++)
        {
            float pdelta = (psys.transform.TransformPoint(particles[i].position) - transform.position).magnitude;
            if (pdelta > delta)
            {
                particles[i].remainingLifetime = 0;
            }
        }
        psys.SetParticles(particles);
    }
}
