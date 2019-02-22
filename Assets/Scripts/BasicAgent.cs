using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class BasicAgent : Agent {

    // exposed parameters
    public float initialHitPoint = 100;
    public float hitPoint;

    public List<Weapon> weapons;
    public int currentWeaponIndex = 0;
    
    // self reference
    private AgentMovement agentMovement;
    
    public BasicAgent() {
        onDeath += DestroySelf;
    }

    protected void Awake() {
        hitPoint = initialHitPoint;
        foreach (Weapon wep in weapons) {
            wep.SetHost(this);
        }
        if (HasWeapon()) {
            ModWeaponIndex(ref currentWeaponIndex);
        }

        agentMovement = GetComponent<AgentMovement>();
    }

    public Weapon GetCurrentWeapon() {
        if (!HasWeapon()) return null;
        return weapons[currentWeaponIndex];
    }

    protected bool HasWeapon() {
        return weapons.Count > 0;
    }

    protected void ModWeaponIndex(ref int index) {
        index = ((index % weapons.Count) + weapons.Count) % weapons.Count;
    }

    public override void ReceiveEvent(Event.Damage damage) {
        hitPoint -= damage.amount;
        if (agentMovement)
        {
            agentMovement.ReceiveEvent(new AgentMovement.Event.ApplyForce { force = damage.force * damage.forceDirection });
        }
        if (hitPoint <= 0) {
            OnDeath();
        }
    }

    public override void ReceiveEvent(Event.FireWeapon fireWeapon) {
        if (!HasWeapon()) return;
        int index = currentWeaponIndex + fireWeapon.indexDelta;
        ModWeaponIndex(ref index);
        weapons[index].Fire();
    }

    public override void ReceiveEvent(Event.SelectNextWeapon selectNextWeapon) {
        if (!HasWeapon()) return;
        currentWeaponIndex += selectNextWeapon.indexDelta;
        ModWeaponIndex(ref currentWeaponIndex);
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
        if (!HasWeapon()) {
            currentWeaponIndex = 0;
        }
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
