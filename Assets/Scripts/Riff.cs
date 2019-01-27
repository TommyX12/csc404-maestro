using System;
using System.Collections.Generic;
using UnityEngine;

public class Riff {

    private float riffSize;
    private float phase;
    private int cycle;
    public float hitMarginBefore = 0.2; // in seconds
    public float hitMarginAfter = 0.1; // in seconds

    public class Note {
        float phase; // position, in beats relative to start of rift
    }

    public struct ButtonHitResult {
        public int noteID = -1; // -1 means too early
        public float deltaTime; // actual press time - desired press time
    }
    
    // notes have to be sorted by phase
    public Riff(float riffSize,
                List<Note> notes) {
        this.riffSize = riffSize;
        this.notes = notes;
        
        phase = 0;
        cycle = 0;
    }

    public void Update(MusicManager musicManager) {
        phase = musicManager.GetCurrentPhase(riffSize);
        cycle = musicManager.GetCurrentCycle(riffSize);
    }

    public float GetPhase() {
        return phase;
    }

    public int GetCycle() {
        return cycle;
    }

    public ButtonHitResult ButtonPress() {
        return null; // TODO
    }
    
}
