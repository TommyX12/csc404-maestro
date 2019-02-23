using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LerpMovement))]
public class SpinnerEnemy : BasicAgent
{
    public PoolableAudioSource deathNoisePrefab;

    public int beatsPerTransition = 1;

    public List<GameObject> patrolPoints;
    int patrolIndex = -1;

    public List<float> rotations;
    int rotationIndex = -1;

    public bool active = false;
    public GameObject spinnerGameObject;
    Animator animator;
    // get the camera mode.

    public LerpMovement movementComponent;
    public LerpRotate spinnerRotateComponent;

    private BasicWeapon weapon;

    private void Start()
    {
        this.type = Agent.Type.ENEMY;
        movementComponent = GetComponent<LerpMovement>();
        // unparent any patrol points
        foreach (GameObject patrolPoint in patrolPoints)
        {
            patrolPoint.transform.SetParent(null);
        }
        if (patrolPoints.Count > 0)
        {
            patrolIndex = 0;
            movementComponent.SetTargetPosition(patrolPoints[0].transform.position);
        }
        else {
            movementComponent.enabled = false;
        }

        if (rotations.Count > 0) {
            rotationIndex = 0;
        } else {
            rotationIndex = -1;
        }
        
        movementComponent.moveTime =  60f / MusicManager.Current.bpm / 2f;
        spinnerRotateComponent.moveTime = 60f / MusicManager.Current.bpm / 2f;
        weapon = GetComponent<BasicWeapon>();
        weapon.SetHost(this);
    }

    public void Update()
    {
        if (active)
        {
            weapon.SetAutoFire(true);
        }
        else {
            weapon.SetAutoFire(false);
        }
    }

    public void GoToNextPoint()
    {
        if (!active) {
            return;
        }
        if (patrolIndex != -1) {
            patrolIndex = (patrolIndex + 1) % patrolPoints.Count;
            movementComponent.SetTargetPosition(patrolPoints[patrolIndex].transform.position);
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
        }
    }
}

