using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

/// <summary>
///   Make sure when overriding the magic methods, call the base (super) method.
/// </summary>
public class Agent : MonoBehaviour {

    public class Event {
        public struct Damage {
            public float amount;
        }
    }

    public Agent() {
        
    }

    public virtual void ReceiveEvent(Event.Damage damage) {
        
    }

}
