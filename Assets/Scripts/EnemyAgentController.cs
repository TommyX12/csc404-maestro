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

    public EnemyAgentController() {
        
    }

    protected void Awake() {
        agentMovement = GetComponent<AgentMovement>();
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

        Physics.Raycast(transform.position, delta, out hit, 10);
        if (hit.collider.gameObject.CompareTag("Player")) {
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
        Vector3 delta = player.transform.position - transform.position;
        Vector3 TargetPos = player.transform.position - new Vector3(delta.x, 0, delta.z).normalized * KeepDistance;
        Vector3 TargetDelta = TargetPos - transform.position;
        if (ShouldChasePlayer()) {
            agentMovement.ReceiveEvent(new AgentMovement.Event.DirectionalMove { right = TargetDelta.x, up = TargetDelta.z});
        }
    }
}
