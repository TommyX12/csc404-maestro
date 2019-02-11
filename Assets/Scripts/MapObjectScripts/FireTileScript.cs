using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTileScript : MonoBehaviour
{

    public bool On = false;

    public ParticleSystem[] ParticleSystems;

    public HazardZone KillZone;

    private void Start()
    {
        StateHelper();
    }

    public void Toggle() {
        On = !On;
        StateHelper();
    }

    public void TurnOn() {
        On = true;
        StateHelper();
    }

    public void TurnOff() {
        On = false;
        StateHelper();
    }

    private void StateHelper() {
        if (On)
        {
            foreach (ParticleSystem p in ParticleSystems)
            {
                p.Play();
                KillZone.Active = true;
            }
        }
        else {
            foreach (ParticleSystem p in ParticleSystems)
            {
                p.Stop(true, ParticleSystemStopBehavior.StopEmitting);
                KillZone.Active = false;
            }
        }
    }
}
