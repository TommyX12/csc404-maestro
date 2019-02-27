using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderEnemy : BasicAgent
{
    public float beatsPerTransition = 1;
    public bool active = false;

    [Range(0,10)]
    public float forwardSpeed = 1;

    public GameObject slider;
    public GameObject sliderPos1;
    public GameObject sliderPos2;
    public LerpMovement movement;
    public BasicWeapon weapon;

    private int index = 0;
    private float progress = 0;

    GameObject[] indices = new GameObject[2];
    private new void Start()
    {
        this.type = Agent.Type.ENEMY;
        indices[0] = sliderPos1;
        indices[1] = sliderPos2;
        movement.moveTime = (60f / MusicManager.current.bpm) * beatsPerTransition;
        movement.SetTargetPosition(indices[0].transform.localPosition);
        weapon.SetHost(this);

        AgentManager.current.AddAgent(this);
    }

    private new void Update()
    {
        if (active)
        {
            weapon.SetAutoFire(true);
        }
        else
        {
            weapon.SetAutoFire(false);
        }
    }

    private void FixedUpdate()
    {
        if (!active) {
            return;
        }
        transform.position += Time.fixedDeltaTime * transform.forward * forwardSpeed;
    }

    public void GoToNextPoint() {
        if (!active) {
            return;
        }
        index = (index + 1)%indices.Length;
        movement.SetTargetPosition(indices[index].transform.localPosition);
    }
}
