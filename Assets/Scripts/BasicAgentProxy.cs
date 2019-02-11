using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAgentProxy : Agent
{
    public BasicAgent Proxy;

    public override void ReceiveEvent(Event.Damage damage)
    {
        Proxy.ReceiveEvent(damage);
    }
}
