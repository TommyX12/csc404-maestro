using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyEnemy : BasicAgent
{
    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        this.type = Agent.Type.ENEMY;
        AgentManager.current.AddAgent(this);
    }
}
