using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class BasicAgent : Agent {

    // exposed parameters
    public float initialHitPoint = 100;
    public float hitPoint;

    public List<Weapon> weapons;
    
    // self reference
    
    public BasicAgent() {
        onDeath += DestroySelf;
    }

    protected void Awake() {
        hitPoint = initialHitPoint;
    }

    public override void ReceiveEvent(Event.Damage damage) {
        hitPoint -= damage.amount;
        if (hitPoint <= 0) {
            OnDeath();
        }
    }

    public override void ReceiveEvent(Event.FireWeapon fireWeapon) {
        foreach (var weapon in weapons) {
            weapon.Fire();
        }
    }

    public override void ReceiveEvent(Event.AimAt aimAt) {
        foreach (var weapon in weapons) {
            weapon.transform.LookAt(aimAt.target);
        }
    }

    public override void ReceiveEvent(Event.AddWeapon addWeapon) {
        AddWeapon(addWeapon.weapon);
    }
    
    protected void DestroySelf(Agent agent) {
        GameObject.Destroy(agent.gameObject);
    }

    public void AddWeapon(Weapon weapon) {
        weapons.Add(weapon);
        weapon.SetHost(this);
    }

    public void RemoveWeapon(Weapon weapon) {
        for (int i = 0; i < weapons.Count; ++i) {
            if (weapons[i] == weapon) {
                weapon.SetHost(null);
                weapons.RemoveAt(i);
                break;
            }
        }
    }
}
