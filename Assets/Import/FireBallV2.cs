using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallV2 : DamageSource
{
    public List<ParticleSystem> destroyEmit = new List<ParticleSystem>();
    public List<ParticleSystem> destroyExit = new List<ParticleSystem>();
    public ParticleSystem explosion;

    public GameObject ParticleGameObject;
    public float speed = 3;

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
        else
        {
            DestroyKeepingTrail();
        }
    }

    private void DestroyKeepingTrail()
    {

        if (ParticleGameObject)
        {
            foreach (ParticleSystem psystem in destroyEmit)
            {
                psystem.Stop(true, ParticleSystemStopBehavior.StopEmitting);
            }

            foreach (ParticleSystem psystem in destroyExit)
            {
                psystem.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
            }

            if (explosion) {
                explosion.Play();
            }

            ParticleGameObject.transform.SetParent(null);
            GameObject.Destroy(ParticleGameObject, 5);
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
