using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

/// <summary>
///   Make sure when overriding the magic methods, call the base (super) method.
/// </summary>
public class Agent : MonoBehaviour {

    public delegate void AgentDeathHandler(Agent agent);

    public event AgentDeathHandler onDeath;

    public Type type = Type.NONE;

    private static int nextUID = 0;

    private int uid;

    public Agent() {
        uid = nextUID++;
    }

    public int GetUID() {
        return uid;
    }

    protected virtual void OnDeath() {
        onDeath(this);
    }

    public virtual Riff GetRiff() {
        return null;
    }

    public virtual void ReceiveEvent(Event.Damage damage) {}
    public virtual void ReceiveEvent(Event.FireWeapon fireWeapon) {}
    public virtual void ReceiveEvent(Event.FireCountermeasure fireCountermeasure) {}
    public virtual void ReceiveEvent(Event.SelectNextWeapon selectNextWeapon) {}
    public virtual void ReceiveEvent(Event.AddWeapon addWeapon) {}
    public virtual void ReceiveEvent(Event.SelectNextCountermeasure selectNextCountermeasure) {}
    public virtual void ReceiveEvent(Event.AddCountermeasure addCountermeasure) {}
    public virtual void ReceiveEvent(Event.AimAt aimAt) {}

    public class Event {
        [Serializable]
        public struct Damage {
            public float amount;
            public float force;
            public Vector3 forceDirection;

            public Damage WithForceDirection(Vector3 forceDirection) {
                this.forceDirection = forceDirection;
                return this;
            }
        }

        public struct FireWeapon {
            public int indexDelta;
        }
        
        public struct FireCountermeasure {
            public int indexDelta;
        }
        
        public struct SelectNextWeapon {
            public int indexDelta;
        }
        
        public struct SelectNextCountermeasure {
            public int indexDelta;
        }
        
        public struct AddWeapon {
            public Weapon weapon;
        }

        public struct AddCountermeasure {
            public Countermeasure countermeasure;
        }
        
        public struct AimAt {
            public Transform target;
        }
    }

    public enum Type {
        NONE, PLAYER, ENEMY
    }
}
