using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class AgentMovement : MonoBehaviour {

    public class Event {
        public struct DirectionalMove {
            public float up;
            public float right;
        }
    }

    public virtual void ReceiveEvent(Event.DirectionalMove directionalMove) {
        
    }

}
