using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using XNode;

public abstract class BasicTemporalNode : TemporalNodeBase {

    [Input(connectionType = ConnectionType.Override)]
    public float time;

    [Output]
    public float afterTime;

    [Output]
    public float value;
    
    public BasicTemporalNode() {
        
    }

    public virtual object GetValue() {
        return GetValue("value");
    }

    public virtual object GetAfterTime() {
        return GetValue("afterTime");
    }
}
