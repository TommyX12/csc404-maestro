using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballScript : DamageSource
{

    public GameObject particleTrail;
    public GameObject explosionObject;
    public ParticleSystem explosionSystem;

    private void OnTriggerEnter(Collider other)
    {
        Damageable dm;
        if ((dm = other.GetComponent<Damageable>()))
        {
            if ((dm.DamageFilter & this.DamageFilter) != 0)
            {
                dm.OnHit(this);
                DestroyKeepingTrail();
            }
        }
        else {
            DestroyKeepingTrail();
        }        
    }

    private void DestroyKeepingTrail() {
        if (particleTrail)
        {
            particleTrail.GetComponent<ParticleSystem>().Stop();
            particleTrail.transform.SetParent(null);
            GameObject.Destroy(particleTrail, 5);
        }
        if (explosionSystem)
        {
            explosionObject.transform.SetParent(null);
            explosionSystem.Play();
            GameObject.Destroy(explosionObject, 1);
        }
        GameObject.Destroy(gameObject);
    }

    private void Start()
    {
        OnEnable();
    }

    private void OnEnable()
    {
        this.GetComponent<Rigidbody>().velocity = transform.forward * 10;
    }
}
