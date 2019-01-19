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

    protected float spawnToPressTime;

    public class ButtonHitResult {
        public int spriteID;
        public int deltaTime; // actual - desired
    }
    
    public SequenceEventEmitter(float spawnToPressTime) {
        this.spawnToPressTime = spawnToPressTime;
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
        if (spriteSpawnEvent != null) {
            spriteSpawnEvent(buttonID, spriteID, offset);
        }
    }

    protected void emitMissedBeatEvent(int spriteID, float offset) {
        if (missedBeatEvent != null) {
            missedBeatEvent(spriteID, offset);
        }
    }

    protected void emitStartMusicEvent(float offset) {
        if (startMusicEvent != null) {
            startMusicEvent(offset);
        }
    }

    abstract protected ButtonHitResult ButtonPress(int buttonID);
}
