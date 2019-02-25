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

    private Riff riff;
    
    public BasicAgent() {
        onDeath += DestroySelf;
    }

    protected void Awake() {
        SetupRiff();
        
        hitPoint = initialHitPoint;
        
        foreach (Weapon weapon in weapons) {
            weapon.SetHost(this);
        }
        
        if (HasWeapon()) {
            ModWeaponIndex(ref currentWeaponIndex);
        }
        else {
            Weapon weapon = GetComponent<Weapon>();
            if (weapon) {
                AddWeapon(weapon);
            }
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

    protected void SetupRiff() {
        List<Riff.Note> notes = new List<Riff.Note>();
        string rhythmDefaultSound = null;

        BasicAgentRhythm rhythm = GetComponent<BasicAgentRhythm>();
        if (rhythm) {
            notes = rhythm.notes;
            rhythmDefaultSound = rhythm.rhythmDefaultSound;
        }

        if (notes == null || notes.Count == 0) {
            notes = Riff.Note.MakeRandomNotes(4, 2, 4);
        }
        riff = new Riff(4, notes, MusicManager.Current);
        if (rhythmDefaultSound == null || rhythmDefaultSound == "") {
            riff.defaultSound = "clap-2";
        }
        else {
            riff.defaultSound = rhythmDefaultSound;
        }
    }

    public override Riff GetRiff() {
        return riff;
        // Weapon weapon = GetCurrentWeapon();
        // return weapon ? weapon.GetRiff() : null;
    }

    protected void ModWeaponIndex(ref int index) {
        index = ((index % weapons.Count) + weapons.Count) % weapons.Count;
    }

    protected void Update() {
        riff.Update();
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

    public override void ReceiveEvent(Event.FireCountermeasure fireCountermeasure) {
        // TODO not implemented
        Debug.Log("FireCountermeasure called");
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
