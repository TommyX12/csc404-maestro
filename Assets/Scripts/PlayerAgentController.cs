using System;
using System.Collections;
using System.Collections.Generic;

using Zenject;

using UnityEngine;

public class PlayerAgentController : AgentController {

    protected Agent target;

    // exposed parameters
    public float targetRadius = 5.0f;
    
    // self reference
    private AgentMovement agentMovement;
    private BasicAgent agent;

    // Injected references
    private GameplayModel model;

    public PlayerAgentController() {

    }

    [Inject]
    public void Construct(GameplayModel model) {
        this.model = model;
    }

    public BasicAgent GetAgent() {
        return agent;
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
        Agent newTarget = AgentManager.current.FindClosestAgentTo(
            transform.position,
            Agent.Type.ENEMY,
            delegate(Agent agent) {
                return // agent != target &&
                    IsValidTarget(agent);
            }
        );
        // if (newTarget) {
        target = newTarget;
        // }
    }

    public Weapon GetCurrentWeapon() {
        return agent.GetCurrentWeapon();
    }

    public Countermeasure GetCurrentCountermeasure() {
        return agent.GetCurrentCountermeasure();
    }

    protected void UpdateTarget() {
        // if (!IsValidTarget(target)) {
        //     target = null;
        AcquireNextTarget();
        // }
        if (target) {
            agent.ReceiveEvent(new Agent.Event.AimAt {target = target.transform});
        }
        Countermeasure countermeasure = GetCurrentCountermeasure();
        if (countermeasure) {
            countermeasure.SetTarget(target);
        }
    }

    public void AddWeapon(Weapon weapon) {
        agent.ReceiveEvent(new Agent.Event.AddWeapon {weapon = weapon});
    }

    public void AddCountermeasure(Countermeasure countermeasure) {
        agent.ReceiveEvent(new Agent.Event.AddCountermeasure {countermeasure = countermeasure});
    }

    public Agent GetTarget() {
        return target;
    }

    protected void UpdateUI() {
        model.PlayerHealth = agent.hitPoint;
        model.PlayerTotalHealth = agent.initialHitPoint;
    }

    protected void Update() {
        UpdateTarget();
        UpdateUI();

        if (ControllerProxy.GetButtonDown("Fire1")) {
            agent.ReceiveEvent(new Agent.Event.FireWeapon());
        }
        if (ControllerProxy.GetButtonDown("Fire2")) {
            agent.ReceiveEvent(new Agent.Event.FireCountermeasure());
        }
    }

    protected void FixedUpdate() {
        float horizontal = ControllerProxy.GetAxisRaw("Horizontal");
        float vertical = ControllerProxy.GetAxisRaw("Vertical");
        agentMovement.ReceiveEvent(new AgentMovement.Event.DirectionalMove {
            up = vertical,
            right = horizontal,
        });
    }
}
