using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public abstract class AgentMovement : MonoBehaviour {

    public class Event {
        public struct DirectionalMove {
            public float up;
            public float right;
        }
        
        public struct LookAt {
            public Vector3 position;
        }
    }

    public abstract void ReceiveEvent(Event.DirectionalMove directionalMove);
    public abstract void ReceiveEvent(Event.LookAt lookAt);

}
