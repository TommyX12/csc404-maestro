using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class BasicAgent : Agent {

    private float hitPoint;

    // exposed parameters
    public float initialHitPoint = 100;

    public List<GameObject> WeaponPositions;
    public List<Weapon> Weapons;
    
    // self reference
    
    public BasicAgent() {
        
    }

    public override void ReceiveEvent(Event.Damage damage) {
        hitPoint -= damage.amount;
        if (hitPoint <= 0) {
            OnDeath();
        }
    }
    
    public virtual void OnDeath() {
        GameObject.Destroy(this);
    }

    public bool AddWeapon(Weapon w) {
        if (Weapons.Count < WeaponPositions.Count) {
            Weapons.Add(w);
            w.gameObject.transform.SetParent(WeaponPositions[Weapons.Count - 1].transform);
            w.gameObject.transform.localPosition = Vector3.zero;
            return true;
        }
        else {
            return false;
        }
    }
}
