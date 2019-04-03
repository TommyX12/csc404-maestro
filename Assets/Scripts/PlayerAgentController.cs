using System;
using System.Collections;
using System.Collections.Generic;

using Zenject;

using UnityEngine;
using UnityEngine.Assertions;

public class PlayerAgentController : AgentController {

    protected Agent target;

    // exposed parameters
    public float targetRadius = 5.0f;
    
    // self reference
    private BasicAgentMovement agentMovement;
    private BasicAgent agent;

    // Injected references
    private GameplayModel model;
    private GlobalConfiguration config;

    // fields
    private bool doubleShotEnabled = false;
    private BasicWeapon doubleShotWeapon = null;
    private float oldSpeed = -1;

    public PlayerAgentController() {

    }

    [Inject]
    public void Construct(GameplayModel model,
                          GlobalConfiguration config) {
        this.model = model;
        this.config = config;
    }

    public BasicAgent GetAgent() {
        return agent;
    }

    protected void Awake() {
        agentMovement = GetComponent<BasicAgentMovement>();
        agent = GetComponent<BasicAgent>();

        Assert.IsNotNull(agentMovement, "agentMovement");
        Assert.IsNotNull(agent, "agent");
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
        Countermeasure countermeasure = GetCurrentCountermeasure();
        if (countermeasure) {
            countermeasure.SetTarget(target);
        }
    }

    protected void AimAtCurrentTarget() {
        if (target) {
            agent.ReceiveEvent(new Agent.Event.AimAt {target = target.transform});
        }
    }

    protected void AimAtSecondTarget() {
        if (!doubleShotWeapon) return;

        Agent newTarget = AgentManager.current.FindClosestAgentTo(
            transform.position,
            Agent.Type.ENEMY,
            delegate(Agent agent) {
                return agent != target &&
                    IsValidTarget(agent);
            }
        );
        if (newTarget) {
            doubleShotWeapon.AimAtSecondary(newTarget.transform);
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
            if (doubleShotEnabled) {
                AimAtSecondTarget();
            }
            AimAtCurrentTarget();
            agent.ReceiveEvent(new Agent.Event.FireWeapon());
        }
        if (ControllerProxy.GetButtonDown("Fire2")) {
            agent.ReceiveEvent(new Agent.Event.FireCountermeasure());
        }
    }

    public void SetDoubleShotEnabled(bool value) {
        doubleShotEnabled = value;
        if (value) {
            doubleShotWeapon = ((BasicWeapon) GetCurrentWeapon());
        }
        if (doubleShotWeapon) {
            doubleShotWeapon.SetDoubleShot(value);
        }
    }

    public void SetSpeedBoostEnabled(bool value) {
        if (value) {
            if (oldSpeed < 0) {
                oldSpeed = agentMovement.maxSpeedPerSecond;
                agentMovement.maxSpeedPerSecond *= config.PowerupSpeedBoostMultiplier;
            }
        }
        else {
            if (oldSpeed >= 0) {
                agentMovement.maxSpeedPerSecond = oldSpeed;
                oldSpeed = -1;
            }
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
