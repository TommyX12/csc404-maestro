using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeakerEnemy : BasicAgent
{
    public PoolableAudioSource deathNoisePrefab;
    public ParticleGroup deathExplosion;

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
    void Update()
    {
        
    }
}
