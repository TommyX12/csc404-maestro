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

    public Agent() {
        
    }

    protected void OnDeath() {
        onDeath(this);
    }

    public virtual void ReceiveEvent(Event.Damage damage) {}
    public virtual void ReceiveEvent(Event.FireWeapon fireWeapon) {}
    public virtual void ReceiveEvent(Event.AddWeapon addWeapon) {}
    public virtual void ReceiveEvent(Event.AimAt aimAt) {}

    public class Event {
        public struct Damage {
            public float amount;
        }

        public struct FireWeapon {}
        public struct AddWeapon {
            public Weapon weapon;
        }

        public struct AimAt {
            public Transform target;
        }
    }

    public enum Type {
        NONE, PLAYER, ENEMY
    }
}
