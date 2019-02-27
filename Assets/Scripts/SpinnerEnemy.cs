using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LerpMovement))]
public class SpinnerEnemy : BasicAgent
{
    public PoolableAudioSource deathNoisePrefab;
    public ParticleGroup deathExplosion;

    public float beatsPerTransition = 1;
    [Range(0,10)]
    public float forwardSpeed = 1;

    public int jumps = 5;

    public List<float> rotations;
    int rotationIndex = -1;

    public float closeness = 10;

    public bool active = false;
    public GameObject spinnerGameObject;
    Animator animator;
    // get the camera mode.

    public LerpMovement movementComponent;
    public LerpRotate spinnerRotateComponent;

    private BasicWeapon weapon;

    private new void Start()
    {
        this.type = Agent.Type.ENEMY;
        movementComponent = GetComponent<LerpMovement>();
        // unparent any patrol points
        if (rotations.Count > 0) {
            rotationIndex = 0;
        } else {
            rotationIndex = -1;
        }
        
        movementComponent.moveTime =  (60f / MusicManager.current.bpm) * beatsPerTransition;
        spinnerRotateComponent.moveTime = (60f / MusicManager.current.bpm)  * beatsPerTransition;
        weapon = GetComponent<BasicWeapon>();
        weapon.SetHost(this);

        Vector2 vector = Random.insideUnitCircle;
        vector = vector.normalized;
        vector *= closeness;
        movementComponent.SetTargetPosition(CombatGameManager.current.player.transform.position + new Vector3(vector.x, 0, vector.y));
        AgentManager.current.AddAgent(this);
    }

    public new void Update()
    {
        if (active)
        {
            weapon.SetAutoFire(true);
        }
        else {
            weapon.SetAutoFire(false);
        }
    }

    private void FixedUpdate()
    {
        if (!active) {
            return;
        }

        transform.position += transform.forward * Time.fixedDeltaTime * forwardSpeed;

    }

    public void GoToNextPoint()
    {
        if (!active) {
            return;
        }
        if (jumps > 0)
        {
            jumps--;
            Vector2 vector = Random.insideUnitCircle;
            vector = vector.normalized;
            vector *= closeness;
            movementComponent.SetTargetPosition(CombatGameManager.current.player.transform.position + new Vector3(vector.x, 0, vector.y));
        }
        else {
            movementComponent.SetTargetPosition(transform.position + Vector3.up * 20);
        }
    }

    public void Rotate() {
        if (!active)
        {
            return;
        }
        if (rotationIndex != -1)
        {
            rotationIndex = (rotationIndex + 1) % rotations.Count;
            spinnerRotateComponent.SetTargetRotation(new Vector3(0,rotations[rotationIndex],0));
        }
    }

    public void Activate() {
        active = true;
    }

    protected override void OnDeath()
    {
        base.OnDeath();
        if (deathNoisePrefab)
        {
            PoolableAudioSource source = AudioSourceManager.current.SpawnAudioSource(deathNoisePrefab);
            if (source) {
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

