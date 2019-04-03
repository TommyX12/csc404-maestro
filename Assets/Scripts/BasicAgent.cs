using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Assertions;

using Zenject;

public class BasicAgent : Agent {

    public int scoreValue = 0;

    // exposed parameters
    public float initialHitPoint = 100;
    public float hitPoint;
    public List<Weapon> weapons;
    public int currentWeaponIndex = 0;
    public List<Countermeasure> countermeasures;
    public int currentCountermeasureIndex = 0;
    
    // self reference
    private AgentMovement agentMovement;

    // Injected references
    protected GameplayModel model;

    private Riff riff;

    public bool Invincible {get; set;}
    
    public BasicAgent() {
        onDeath += AddScore;
        onDeath += DestroySelf;
    }

    [Inject]
    public void Construct(GameplayModel model) {
        this.model = model;
    }

    protected void Awake() {
        hitPoint = initialHitPoint;
        Invincible = false;

        InitWeapon();
        InitCountermeasure();
        
        agentMovement = GetComponent<AgentMovement>();
    }

    protected void InitWeapon() {
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
    }

    protected void InitCountermeasure() {
        foreach (Countermeasure countermeasure in countermeasures) {
            countermeasure.SetHost(this);
        }
        
        if (HasCountermeasure()) {
            ModCountermeasureIndex(ref currentCountermeasureIndex);
        }
        else {
            Countermeasure countermeasure = GetComponent<Countermeasure>();
            if (countermeasure) {
                AddCountermeasure(countermeasure);
            }
        }
    }

    protected void Start() {
        SetupRiff();
    }

    public Weapon GetCurrentWeapon() {
        if (!HasWeapon()) return null;
        return weapons[currentWeaponIndex];
    }

    protected bool HasWeapon() {
        return weapons.Count > 0;
    }

    public Countermeasure GetCurrentCountermeasure() {
        if (!HasCountermeasure()) return null;
        return countermeasures[currentCountermeasureIndex];
    }

    protected bool HasCountermeasure() {
        return countermeasures.Count > 0;
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
        riff = new Riff(4, notes, MusicManager.current);
        if (rhythmDefaultSound == null || rhythmDefaultSound == "") {
            riff.defaultSound = "chord-1";
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

    protected void ModCountermeasureIndex(ref int index) {
        index = ((index % countermeasures.Count) + countermeasures.Count) % countermeasures.Count;
    }

    protected void Update() {
        riff.Update();
    }

    public override void ReceiveEvent(Event.Damage damage) {
        if (Invincible) {
            return;
        }

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
        if (!HasCountermeasure()) return;
        int index = currentCountermeasureIndex + fireCountermeasure.indexDelta;
        ModCountermeasureIndex(ref index);
        countermeasures[index].Fire();
    }

    public override void ReceiveEvent(Event.SelectNextWeapon selectNextWeapon) {
        if (!HasWeapon()) return;
        currentWeaponIndex += selectNextWeapon.indexDelta;
        ModWeaponIndex(ref currentWeaponIndex);
    }

    public override void ReceiveEvent(Event.SelectNextCountermeasure selectNextCountermeasure) {
        if (!HasCountermeasure()) return;
        currentCountermeasureIndex += selectNextCountermeasure.indexDelta;
        ModCountermeasureIndex(ref currentCountermeasureIndex);
    }

    public override void ReceiveEvent(Event.AimAt aimAt) {
        foreach (var weapon in weapons) {
            weapon.AimAt(aimAt.target);
        }
        foreach (var countermeasure in countermeasures) {
            countermeasure.transform.LookAt(aimAt.target);
        }
    }

    public override void ReceiveEvent(Event.AddWeapon addWeapon) {
        AddWeapon(addWeapon.weapon);
    }
    
    public override void ReceiveEvent(Event.AddCountermeasure addCountermeasure) {
        AddCountermeasure(addCountermeasure.countermeasure);
    }
    
    protected void DestroySelf(Agent agent) {
        GameObject.Destroy(agent.gameObject);
    }

    private void AddScore(Agent agent) {
        model.AddScore(this.scoreValue);
        for (int i = 0; i < Mathf.Max(1, scoreValue / 5); i++)
        {
            model.IncrementCombo();
        }

        if (ScoreManager.current) {
            var go = GameObject.Instantiate(ScoreManager.current.floatingScorePrefab);
            var score = go.GetComponent<FloatingScore>();
            score.SetScore(this.scoreValue);
            go.transform.position = this.transform.position;
        }

        if (ScoreManager.current && ScoreManager.current.scoreParticles)
        {
            var pg = ParticleManager.instance.GetParticleGroup(ScoreManager.current.scoreParticles);
            pg.transform.position = agent.transform.position;
            pg.emissionNums[0] = this.scoreValue;
            pg.PlayOnce();
        }
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

    public void AddCountermeasure(Countermeasure countermeasure) {
        if (!HasCountermeasure()) {
            currentCountermeasureIndex = 0;
        }
        countermeasures.Add(countermeasure);
        countermeasure.SetHost(this);
    }

    public void RemoveCountermeasure(Countermeasure countermeasure) {
        for (int i = 0; i < countermeasures.Count; ++i) {
            if (countermeasures[i] == countermeasure) {
                countermeasure.SetHost(null);
                countermeasures.RemoveAt(i);
                break;
            }
        }
    }
}
