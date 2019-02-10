using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class PlayerAgentController : AgentController {

    private AgentMovement agentMovement;
    
    public PlayerAgentController() {

    }

    protected void Awake() {
        agentMovement = GetComponent<AgentMovement>();
    }

    protected void Start() {
        
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
