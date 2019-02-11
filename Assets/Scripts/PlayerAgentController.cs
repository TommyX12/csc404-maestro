using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class PlayerAgentController : AgentController {

    protected Agent target;

    // exposed parameters
    public float targetRadius = 5.0f;
    
    // self reference
    private AgentMovement agentMovement;
    private BasicAgent agent;

    public PlayerAgentController() {

    }

    protected void Awake() {
        agentMovement = GetComponent<AgentMovement>();
        agent = GetComponent<BasicAgent>();
    }

    protected void Start() {
        
    }

    private bool IsValidTarget(Agent agent) {
        return agent != null && AgentManager.AgentInRange(agent, transform.position, targetRadius);
    }

    protected void AcquireNextTarget() {
        target = AgentManager.current.FindClosestAgentTo(
            transform.position,
            Agent.Type.ENEMY,
            delegate(Agent agent) {
                return agent != target && IsValidTarget(agent);
            }
        );
    }

    public Weapon GetCurrentWeapon() {
        return agent.GetCurrentWeapon();
    }

    protected void UpdateTarget() {
        if (!IsValidTarget(target)) {
            AcquireNextTarget();
        }
        if (target) {
            agent.ReceiveEvent(new Agent.Event.AimAt {target = target.transform});
        }
    }

    public void AddWeapon(Weapon weapon) {
        agent.ReceiveEvent(new Agent.Event.AddWeapon {weapon = weapon});
    }

    public Agent GetTarget() {
        return target;
    }

    protected void Update() {
        UpdateTarget();

        if (Input.GetButtonDown("RB")) {
            agent.ReceiveEvent(new Agent.Event.FireWeapon());
        }
    }

    protected void FixedUpdate() {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        agentMovement.ReceiveEvent(new AgentMovement.Event.DirectionalMove {
            up = vertical,
            right = horizontal,
        });
    }
}
