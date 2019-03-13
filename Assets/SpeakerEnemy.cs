using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeakerEnemy : BasicAgent
{
    public PoolableAudioSource deathNoisePrefab;
    public ParticleGroup deathExplosion;

    public float speed;
    public bool moveForward = true;

    private new void Start()
    {
        base.Start(); 
        AgentManager.current.AddAgent(this);
        this.type = Agent.Type.ENEMY;
        foreach (BasicWeapon weapon in weapons) {
            weapon.SetAutoFire(true);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(moveForward) transform.position += transform.forward * speed * Time.fixedDeltaTime;
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
            if (pg)
            {
                pg.transform.position = transform.position;
                pg.PlayOnce();
            }
        }
    }

}
