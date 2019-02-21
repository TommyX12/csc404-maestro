using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpactParticles : MonoBehaviour
{
    public ParticleSystem sys;

    public void OnHit() {
        transform.SetParent(null);
        sys.Play();
        sys.Emit(20);
        GameObject.Destroy(this.gameObject, 2f);
    }

}
