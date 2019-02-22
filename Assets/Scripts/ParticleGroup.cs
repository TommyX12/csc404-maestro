using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleGroup : MonoBehaviour
{
    public List<ParticleSystem> particleSystems;
    public List<int> emissionNums;
    public virtual void PlayOnce() {
        int count = 0;
        foreach (ParticleSystem psys in particleSystems) {
            psys.Emit(emissionNums[count]);
            count++;
        }
    }
}
