using System;
using UnityEngine;

public abstract class SequenceEventEmitter {

    // offset are how "late" the event is.
    // if the event is emitted 0.5 seconds later than intended, offset = 0.5.
    public delegate void SpriteSpawnEventHandler(int buttonID, int spriteID, float offset);
    public delegate void MissedBeatEventHandler(int spriteID, float offset);
    public delegate void StartMusicEventHandler(float offset);

    protected event SpriteSpawnEventHandler spriteSpawnEvent;
    protected event MissedBeatEventHandler missedBeatEvent;
    protected event StartMusicEventHandler startMusicEvent;

    public class ButtonHitResult {
        public int spriteID = -1; // -1 means too early
        public float deltaTime; // actual press time - desired press time
        public bool buttonCorrect;
    }
    
    public SequenceEventEmitter() {
        
    }

    abstract public void Start();

    // call this in fixed update
    abstract public void Step(float deltaTime);
    
    public void addSpriteSpawnEventHandler(SpriteSpawnEventHandler handler) {
        spriteSpawnEvent += handler;
    }

    public void addMissedBeatEventHandler(MissedBeatEventHandler handler) {
        missedBeatEvent += handler;
    }

    public void addStartMusicEventHandler(StartMusicEventHandler handler) {
        startMusicEvent += handler;
    }

    protected void emitSpriteSpawnEvent(int buttonID, int spriteID, float offset) {
        Debug.Log("spawn");
        if (spriteSpawnEvent != null) {
            spriteSpawnEvent(buttonID, spriteID, offset);
        }
    }

    protected void emitMissedBeatEvent(int spriteID, float offset) {
        Debug.Log("missed");
        if (missedBeatEvent != null) {
            missedBeatEvent(spriteID, offset);
        }
    }

    protected void emitStartMusicEvent(float offset) {
        Debug.Log("start music");
        if (startMusicEvent != null) {
            startMusicEvent(offset);
        }
    }

    abstract protected ButtonHitResult ButtonPress(int buttonID);
}
