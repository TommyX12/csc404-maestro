using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class CombatGameManager : MonoBehaviour {
    
    public static CombatGameManager current;
    
    // references
    public MusicManager musicManager;
    public string track = "csc404-test-base";
    public int trackBPM = 80;
    public PlayerAgentController player;

    protected void Awake() {
        CombatGameManager.current = this;
        DontDestroyOnLoad(this);
    }
    
    protected void Start() {
        musicManager.StartRiff(track, trackBPM);
        // musicManager.PlayPattern("csc404-test-weapon-1", 4);
    }

    protected void FixedUpdate() {
        
    }

    protected void Update() {
        
    }
}
