using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class EnemyAgentController : AgentController {

    private AgentMovement agentMovement;
    
    public EnemyAgentController() {
        
    }

    protected void Awake() {
        agentMovement = GetComponent<AgentMovement>();
    }

    protected void Start() {
        
    }

    protected void Update()
    {
        
    }

    protected void FixedUpdate() {
        
    }
}
