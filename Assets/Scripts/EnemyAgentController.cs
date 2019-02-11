using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class EnemyAgentController : AgentController {

    private AgentMovement agentMovement;
    
    public EnemyAgentController() {
        
    }

    protected new void Awake() {
        agentMovement = GetComponent<AgentMovement>();
    }

    protected new void Start() {
        
    }

    protected new void Update()
    {
        
    }

    protected void FixedUpdate() {
        
    }
}
