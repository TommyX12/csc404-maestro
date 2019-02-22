using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// currently 1 pools.
// So bad for temporally sensitive particle effects
public class ParticleManager : MonoBehaviour
{
    public static ParticleManager instance;

    private Dictionary<string, ParticleGroup> particles = new Dictionary<string, ParticleGroup>();

    private void Awake()
    {
        instance = this;
    }

    public ParticleGroup GetParticleGroup(ParticleGroup pg) {
        if (particles.ContainsKey(pg.name))
        {
            return particles[pg.name];
        }
        else {
            particles.Add(pg.name, GameObject.Instantiate(pg).GetComponent<ParticleGroup>());
            return particles[pg.name];
        }
    }
}
