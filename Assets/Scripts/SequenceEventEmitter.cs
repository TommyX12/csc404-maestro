using System;
using UnityEngine;

public abstract class SequenceEventEmitter {

    public delegate void SpriteSpawnEventHandler(int buttonID, int spriteID);
    public delegate void MissedBeatEventHandler(int spriteID);
    public delegate void StartMusicEventHandler();

    protected event SpriteSpawnEventHandler spriteSpawnEvent;
    protected event MissedBeatEventHandler missedBeatEvent;
    protected event StartMusicEventHandler startMusicEvent;

    protected float currentTime;

    public class ButtonHitResult {
        public int spriteID;
        public int deltaTime; // actual - desired
    }
    
    public SequenceEventEmitter() {
        
    }

    // call this in fixed update
    public void Step(float deltaTime) {
        currentTime += deltaTime;
        InternalStep(deltaTime);
    }

    abstract protected void InternalStep(float deltaTime);

    public void addSpriteSpawnEventHandler(SpriteSpawnEventHandler handler) {
        spriteSpawnEvent += handler;
    }

    protected void emitSpriteSpawnEvent(int buttonID, int spriteID) {
        if (spriteSpawnEvent != null) {
            spriteSpawnEvent(buttonID, spriteID);
        }
    }

    protected void emitMissedBeatEvent(int spriteID) {
        if (missedBeatEvent != null) {
            missedBeatEvent(spriteID);
        }
    }

    protected void emitStartMusicEvent() {
        if (startMusicEvent != null) {
            startMusicEvent();
        }
    }

    abstract protected ButtonHitResult ButtonPress(int buttonID);
}
