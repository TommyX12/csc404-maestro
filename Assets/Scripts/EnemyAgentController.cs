using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class EnemyAgentController : AgentController {

    private AgentMovement agentMovement;

    public float CutoffRange = 10f;
    public float VisionDistance = 5f;
    public float KeepDistance = 2.5f;
    private float ChaseTimer = 1f;

    // self reference
    private BasicWeapon basicWeapon;

    public EnemyAgentController() {
        
    }

    protected void Awake() {
        agentMovement = GetComponent<AgentMovement>();
        basicWeapon = GetComponent<BasicWeapon>();
    }

    protected void Start() {
        
    }

    // checks if player is detected
    bool ShouldChasePlayer() {

        // decrement chase timer
        if (ChaseTimer > 0) {
            ChaseTimer -= Time.deltaTime;
        }

        PlayerAgentController player = CombatGameManager.current.player;
        Vector3 delta = player.transform.position - transform.position;
        // distance check
        if (delta.magnitude > CutoffRange) {
            ChaseTimer = 0;
            return false;
        }

        // raycast to player
        RaycastHit hit;

        Physics.Raycast(transform.position, delta, out hit, VisionDistance);
        if (hit.collider && hit.collider.gameObject.CompareTag("Player")) {
            ChaseTimer = 1;
            return true;
        }

        if (ChaseTimer > 0) {
            return true;
        }

        return false;
    }

    protected void Update()
    {

    }

    protected void FixedUpdate() {
        PlayerAgentController player = CombatGameManager.current.player;
        if (player == null) {
            return;
        }
        Vector3 delta = player.transform.position - transform.position;
        Vector3 TargetPos = player.transform.position - new Vector3(delta.x, 0, delta.z).normalized * KeepDistance;
        Vector3 TargetDelta = TargetPos - transform.position;
        if (ShouldChasePlayer()) {
            basicWeapon.SetAutoFire(true);
            agentMovement.ReceiveEvent(new AgentMovement.Event.DirectionalMove { right = TargetDelta.x, up = TargetDelta.z});
            AgentMovement.Event.LookAt aim;
            aim.position = player.transform.position;
            agentMovement.ReceiveEvent(aim);
        }
        else {
            basicWeapon.SetAutoFire(false);
        }
    }
}
