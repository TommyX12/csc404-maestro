using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderEnemy : BasicAgent
{
    public float beatsPerTransition = 1;
    public bool active = false;
    public GameObject slider;
    public GameObject sliderPos1;
    public GameObject sliderPos2;
    public LerpMovement movement;
    public BasicWeapon weapon;

    private int index = 0;
    private float progress = 0;

    GameObject[] indices = new GameObject[2];
    private void Start()
    {
        this.type = Agent.Type.ENEMY;
        indices[0] = sliderPos1;
        indices[1] = sliderPos2;
        movement.moveTime = (60f / MusicManager.Current.bpm) * beatsPerTransition;
        movement.SetTargetPosition(indices[0].transform.position);
        weapon.SetHost(this);

        AgentManager.current.AddAgent(this);
    }

    private void Update()
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

    public void GoToNextPoint() {
        if (!active) {
            return;
        }
        index = (index + 1)%indices.Length;
        movement.SetTargetPosition(indices[index].transform.position);
    }
}
