using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiralEnemy : BasicAgent
{
    public PoolableAudioSource deathNoisePrefab;
    public ParticleGroup deathExplosion;
    public GameObject rotator;
    [Range(1, 10)]
    public int beatsPerRotation = 4;

    [Range(0, 10)]
    public float forwardSpeed;

    public bool active = false;

    private new void Start()
    {
        base.Start();
        AgentManager.current.AddAgent(this);
    }

    private void FixedUpdate()
    {
        if (!active) {
            foreach (BasicWeapon w in weapons) {
                w.SetAutoFire(false);
            }
            return;
        }

        foreach (BasicWeapon w in weapons)
        {
            w.SetAutoFire(true);
        }
        transform.position += transform.forward * forwardSpeed * Time.fixedDeltaTime;
        rotator.transform.rotation = rotator.transform.rotation * Quaternion.Euler(0, (Time.fixedDeltaTime * MusicManager.current.bpm/60f)*360f/beatsPerRotation, 0);
    }

    protected override void OnDeath()
    {
        base.OnDeath();
        if (deathNoisePrefab)
        {
            PoolableAudioSource source = AudioSourceManager.current.SpawnAudioSource(deathNoisePrefab);
            if (source)
            {
                source.transform.position = this.transform.position;
                source.StartCoroutine("Play");
            }

            ParticleGroup pg = ParticleManager.instance.GetParticleGroup(deathExplosion);
            if (pg) {
                pg.transform.position = transform.position;
                pg.PlayOnce();
            }
        }
    }
}
